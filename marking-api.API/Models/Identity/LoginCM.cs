using marking_api.Data;
using marking_api.DataModel.API;
using marking_api.DataModel.Identity;
using marking_api.Global.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace marking_api.API.Models.Identity
{
    public class LoginCM : BaseModel
    {
        public MarkingDbContext _markingDbContext;
        public SignInManager<User> _signInManager;
        public Jwt _jwt;
        public TokenValidationParameters _tokenValidationParameters;
        public LoginCM(MarkingDbContext markingDbContext, SignInManager<User> signInManager, Jwt jwt, TokenValidationParameters tokenValidationParameters)
        {
            _markingDbContext = markingDbContext;
            _signInManager = signInManager;
            _jwt = jwt;
            _tokenValidationParameters = tokenValidationParameters;            
        }

        public AuthRequest GenerateJwtToken(IdentityUser user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwt.Secret);

            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.Now.AddMonths(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = handler.CreateToken(tokenDesc);
            var jwtToken = handler.WriteToken(token);

            var refreshToken = new RefreshTokenDM()
            {
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = user.Id,
                AddedDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddMonths(6),
                Token = UtilityExtensions.GenerateRefreshToken()
            };

            _markingDbContext.RefreshTokens.Add(refreshToken);
            _markingDbContext.SaveChanges();

            return new AuthRequest()
            {
                Token = jwtToken,
                Success = true,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<AuthRequest> VerifyAndGenerateToken(TokenRequest tokenRequest)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var tokenVerification = handler.ValidateToken(tokenRequest.BearerToken, _tokenValidationParameters, out var validatedToken);
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (result == false)
                        return null;
                }

                var unixTimeStamp = long.Parse(tokenVerification.Claims.FirstOrDefault(x => x.Type.Equals(JwtRegisteredClaimNames.Exp)).Value);
                var expiryDate = UtilityExtensions.UnixTimeStampToDateTime(unixTimeStamp);
                if (expiryDate > DateTime.UtcNow)
                {
                    return new AuthRequest()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token has not yet expired"
                        }
                    };
                }

                var storedToken = _markingDbContext.RefreshTokens.FirstOrDefault(x => x.Token.Equals(tokenRequest.RefreshToken));
                if (storedToken == null)
                {
                    return new AuthRequest()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token does not exist"
                        }
                    };
                }

                if (storedToken.IsUsed)
                {
                    return new AuthRequest()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token has been used"
                        }
                    };
                }

                if (storedToken.IsRevoked)
                {
                    return new AuthRequest()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token has been revoked"
                        }
                    };
                }

                var jti = tokenVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
                if (storedToken.JwtId != jti)
                {
                    return new AuthRequest()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token doesn't match"
                        }
                    };
                }

                storedToken.IsUsed = true;
                _markingDbContext.RefreshTokens.Update(storedToken);
                _markingDbContext.SaveChanges();

                var user = await _signInManager.UserManager.FindByIdAsync(storedToken.UserId);
                return GenerateJwtToken(user);
            } 
            catch (Exception ex)
            {
                if (ex.Message.Contains("Lifetime validation failed. This tokens is expired"))
                {
                    return new AuthRequest()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Token has expired please re-login"
                        }
                    };
                } else
                {
                    return new AuthRequest()
                    {
                        Success = false,
                        Errors = new List<string>()
                        {
                            "Something went wrong."
                        }
                    };
                }
            }
        }
    }
}
