using log4net.Core;
using marking_api.Data;
using marking_api.DataModel.API;
using marking_api.DataModel.Identity;
using marking_api.Global.Extensions;
using marking_api.Global.Repositories;
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
    /// <summary>
    /// Login controller model
    /// </summary>
    public class LoginCM : BaseModel
    {
        /// <summary>
        /// UnitOfWork database access
        /// </summary>
        public IUnitOfWork _unitOfWork;

        /// <summary>
        /// Identity sign in manager
        /// </summary>
        public SignInManager<User> _signInManager;

        /// <summary>
        /// Jwt secret string
        /// </summary>
        public Jwt _jwt;

        /// <summary>
        /// Jwt validation parameters
        /// </summary>
        public TokenValidationParameters _tokenValidationParameters;

        /// <summary>
        /// Login constructor
        /// </summary>
        /// <param name="unitOfWork">IUnitOfWork</param>
        /// <param name="signInManager">SignInManager(User)</param>
        /// <param name="jwt">Jwt</param>
        /// <param name="tokenValidationParameters">TokenValidationParameters</param>
        public LoginCM(IUnitOfWork unitOfWork, SignInManager<User> signInManager, Jwt jwt, TokenValidationParameters tokenValidationParameters, ILogger logger) : base(logger)
        {
            _unitOfWork = unitOfWork;
            _signInManager = signInManager;
            _jwt = jwt;
            _tokenValidationParameters = tokenValidationParameters;            
        }

        /// <summary>
        /// Generate a jwt token and a refresh token which is saved in the database.
        /// The jwt token is made up of several user claims which are id, email, firstname, lastname, profile picture and a jti guid.
        /// This is sent back in the form of an AuthRequest which contains the jwt token, if token generation was successful and the refresh token
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>AuthRequest</returns>
        public AuthRequest GenerateJwtToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwt.Secret);

            //User claims
            var tokenDesc = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                    new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                    //new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                    new Claim("ProfilePicture", user.ProfilePicture?.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = DateTime.Now.AddMonths(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //Generate jwt token
            var token = handler.CreateToken(tokenDesc);
            var jwtToken = handler.WriteToken(token);

            //Generate refresh token
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
            
            //Save refresh token
            _unitOfWork.RefreshTokens.Add(refreshToken);
            _unitOfWork.Save();          

            //Generate auth request to post back
            return new AuthRequest()
            {
                Token = jwtToken,
                Success = true,
                RefreshToken = refreshToken.Token
            };
        }

        /// <summary>
        /// Verify refresh token and generate new jwt token
        /// Returns no jwt token if the refresh token has an error
        /// </summary>
        /// <param name="tokenRequest">TokenRequest</param>
        /// <returns>AuthRequest containing new jwt token, if success and refresh token</returns>
        public async Task<AuthRequest> VerifyAndGenerateToken(TokenRequest tokenRequest)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                //Test jwt token
                var tokenVerification = handler.ValidateToken(tokenRequest.BearerToken, _tokenValidationParameters, out var validatedToken);
                //If token is valid
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
                    if (result == false)
                        return null;
                }

                //Test if the jwt token has expired
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

                //Test if the refresh token exists in the database
                var storedToken = _unitOfWork.RefreshTokens.Get(filter: x => x.Token.Equals(tokenRequest.RefreshToken)).FirstOrDefault();
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

                //Tests if the refresh token has been used already
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

                //Tests if the refresh token has been revoked
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

                //Tests if the jti matches
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

                //Set refresh token to used
                storedToken.IsUsed = true;
                _unitOfWork.RefreshTokens.Update(storedToken);
                _unitOfWork.Save();

                //Get used and return generated newly generated jwt token
                var user = await _signInManager.UserManager.FindByIdAsync(storedToken.UserId);
                return GenerateJwtToken(user);
            } 
            catch (Exception ex)
            {
                //If refresh token has expired
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
