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
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace marking_api.API
{
    /// <summary>
    /// Startup class which configures and sets up services for the application to run correctly
    /// </summary>
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        /// <summary>
        /// Initiate configuration and environment for use in ConfigureServices and Configure
        /// </summary>
        /// <param name="configuration">IConfiguration</param>
        /// <param name="env">IWebHostEnvironment</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        /// <summary>
        /// Contains configuration properties for the application such as appsettings
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// IWebHostEnvironment contains information about the environment settings that the application is running in
        /// </summary>
        public IWebHostEnvironment Env { get; set; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

            //Switch database connection strings depending on the application environment
            //Ignore schema information due to the difference between MySQL and MSSQL
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

            //Configure local language
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

            //Cors authentication
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

            //Swagger Configuration
            services.AddSwaggerGen(c =>
            {
                //Swagger API name
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "marking_api.API", Version = "v1" });
                //Exclusion filter for DataModel properties
                c.SchemaFilter<SwaggerExcludeFilter>();
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                //Add JWT Authorisation to Swagger testing page 
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

            //JWT authorisation for API requests
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme; //JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>

            {
                options.SaveToken = true;
                options.TokenValidationParameters = tokenValidationParameters;
                if (options.Events != null)
                {
                    options.Events.OnMessageReceived = context =>
                    {
                        if (context.Request.Cookies.ContainsKey("accesstoken") && !(context.Request.Path.ToUriComponent().Contains("login", StringComparison.OrdinalIgnoreCase) || context.Request.Path.ToUriComponent().Contains("refreshtoken", StringComparison.OrdinalIgnoreCase)))
                        {
                            context.Token = context.Request.Cookies["accesstoken"];
                        }
                        return Task.CompletedTask;
                    };
                }
            });

            services.AddAuthorization(options =>
            {
                var authPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                authPolicyBuilder = authPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = authPolicyBuilder.Build();
                options.FallbackPolicy = new AuthorizationPolicyBuilder().AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
            });

            //Database seeder
            services.AddScoped<MarkingDbSeeder>();

            //Global services
            services.AddScoped<UtilService>();

            //UnitOfWork and two main inherited repositories
            services.AddTransient(typeof(IGenericModelRepository<>), typeof(GenericModelRepository<>));
            services.AddTransient(typeof(IGenericViewRepository<>), typeof(GenericViewRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">IApplicationBuilder</param>
        /// <param name="env">IWebHostEnvironment</param>
        /// <param name="dbSeeder">MarkingDbSeeder</param>
        /// <param name="loggerFactory">ILoggerFactory</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MarkingDbSeeder dbSeeder, ILoggerFactory loggerFactory)
        {
            //Swagger API package
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "marking_api.API v1");
            });

            //Seed data into the database
            if (dbSeeder != null)
            {
                //Execute outstanding migrations against the database
                dbSeeder.Migrate();
                //Add data to the database
                dbSeeder.SeedData();
            }

            app.UseExceptionHandler(c => c.Run(async context => 
            {
                var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;
                var response = new { error = exception.Message };
                await context.Response.WriteAsJsonAsync(response);
            }));

            //Redirect to HTTPS from HTTP
            app.UseHttpsRedirection();

            app.UseRouting();

            //Authorisation middleware
            app.UseCors(MyAllowSpecificOrigins);
            
            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //Setup log4net as the logging provider
            var loggingOptions = this.Configuration.GetSection("Log4NetCore").Get<Log4NetProviderOptions>();
            loggerFactory.AddLog4Net(loggingOptions);
        }
    }
}
