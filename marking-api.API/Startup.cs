using marking_api.Data;
using marking_api.DataModel.API;
using marking_api.DataModel.Identity;
using marking_api.Global.Repositories;
using marking_api.Global.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace marking_api.API
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

            if (Env.IsDevelopment())
            {                
                services.AddDbContext<MarkingDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DbConnection"), serverVersion, o => 
                {
                    o.SchemaBehavior(MySqlSchemaBehavior.Ignore);
                    o.EnableRetryOnFailure();
                }));
            } else
            {
                services.AddDbContext<MarkingDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DbConnection"), serverVersion, o => 
                { 
                    o.SchemaBehavior(MySqlSchemaBehavior.Ignore);
                    o.EnableRetryOnFailure(); 
                }));
            }

            services.Configure<RequestLocalizationOptions>(o =>
            {
                var supportedCultures = new[]
                {
                    new CultureInfo("en-GB"),
                };
                o.DefaultRequestCulture = new RequestCulture("en-GB");
                o.SupportedCultures = supportedCultures;
                o.SupportedUICultures = supportedCultures;
            });

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(AppInitialiserFilter));
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddIdentity<User, Role>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
            }).AddDefaultTokenProviders().AddEntityFrameworkStores<MarkingDbContext>();

            services.AddHttpContextAccessor();

            services.AddCors(options => 
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, (builder) => 
                {
                    builder
                        .WithOrigins(Configuration["FrontendCors"])
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                }); 
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "marking_api.API", Version = "v1" });
                c.SchemaFilter<SwaggerExcludeFilter>();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorisation header using the bearer scheme",
                    Name = "Authorisation",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    }, new List<string>() }
                });
            });

            services.Configure<Jwt>(Configuration.GetSection("Jwt"));

            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:Secret"]);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
            });

            services.AddAuthorization(options =>
            {
                var authPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                authPolicyBuilder = authPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = authPolicyBuilder.Build();
                options.FallbackPolicy = new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
            });

            services.AddScoped<MarkingDbSeeder>();
            services.AddScoped<UtilService>();

            services.AddTransient(typeof(IGenericModelRepository<>), typeof(GenericModelRepository<>));
            services.AddTransient(typeof(IGenericViewRepository<>), typeof(GenericViewRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MarkingDbSeeder dbSeeder, ILoggerFactory loggerFactory)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "marking_api.API v1");
            });

            if (dbSeeder != null)
            {
                dbSeeder.Migrate();
                dbSeeder.SeedData();
            }

            app.UseExceptionHandler(c => c.Run(async context => 
            {
                var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
                var response = new { error = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);
            
            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var loggingOptions = this.Configuration.GetSection("Log4NetCore").Get<Log4NetProviderOptions>();
            loggerFactory.AddLog4Net(loggingOptions);
        }
    }
}
