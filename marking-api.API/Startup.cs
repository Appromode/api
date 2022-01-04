using Hangfire;
using Hangfire.SqlServer;
using marking_api.API.Config;
using marking_api.Data;
using marking_api.DataModel.Identity;
using marking_api.Global.Repositories;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
            //var hangfireConnection = "";
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 27));

            if (Env.IsDevelopment())
            {                
                services.AddDbContext<MarkingDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DbConnection"), serverVersion, o => o.SchemaBehavior(MySqlSchemaBehavior.Ignore)));
                //hangfireConnection = "DbConnection";
            } else
            {
                services.AddDbContext<MarkingDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DbConnection"), serverVersion, o => o.SchemaBehavior(MySqlSchemaBehavior.Ignore)));
                //hangfireConnection = "DbConnection";
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

            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme).AddCertificate();

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
                //c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    Description = "JWT Authorisation header using the bearer scheme",
                //    Name = "Authorisation",
                //    In = ParameterLocation.Header,
                //    Type = SecuritySchemeType.ApiKey
                //});
                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    { new OpenApiSecurityScheme
                //    {
                //        Reference = new OpenApiReference
                //        {
                //            Id = "Bearer",
                //            Type = ReferenceType.SecurityScheme
                //        }
                //    }, new List<string>() }
                //});
            });

            //services.AddHangfire(configuration => configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            //.UseSimpleAssemblyNameTypeSerializer()
            //.UseSerializerSettings(new JsonSerializerSettings 
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            //    PreserveReferencesHandling = PreserveReferencesHandling.Objects
            //})
            //.UseSqlServerStorage(Configuration.GetConnectionString(hangfireConnection), new SqlServerStorageOptions 
            //{
            //    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            //    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            //    QueuePollInterval = TimeSpan.Zero,
            //    UseRecommendedIsolationLevel = true,
            //    DisableGlobalLocks = true
            //}));

            //services.AddHangfireServer();

            services.AddScoped<MarkingDbSeeder>();

            services.AddTransient(typeof(IGenericModelRepository<>), typeof(GenericModelRepository<>));
            services.AddTransient(typeof(IGenericViewRepository<>), typeof(GenericViewRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MarkingDbSeeder dbSeeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => 
                { 
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "marking_api.API v1"); 
                });
            } else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            if (dbSeeder != null)
            {
                dbSeeder.Migrate();
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
        }
    }
}
