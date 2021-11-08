using Hangfire;
using Hangfire.SqlServer;
using marking_api.Data;
using marking_api.DataModel.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace marking_api.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env, MarkingDbContext dbContext)
        {
            Configuration = configuration;
            Env = env;
            _dbContext = dbContext;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; set; }
        private MarkingDbContext _dbContext { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var hangfireConnection = "";

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "marking_api.API", Version = "v1" });
                c.SchemaFilter<SwaggerExcludeFilter>();
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

            if (Env.IsDevelopment())
            {
                RelationalDatabaseFacadeExtensions.SetConnectionString(new DatabaseFacade(_dbContext), Configuration.GetConnectionString("DbConnection"));
                services.AddDbContext<MarkingDbContext>(options => options.UseMySql(ServerVersion.AutoDetect(Configuration.GetConnectionString("DbConnection"))));
                hangfireConnection = "DbConnection";
                //options.UseSqlServer(Configuration.GetConnectionString("DbConnection"))
            } else
            {
                RelationalDatabaseFacadeExtensions.SetConnectionString(new DatabaseFacade(_dbContext), Configuration.GetConnectionString("DbConnection"));
                services.AddDbContext<MarkingDbContext>(options => options.UseMySql(ServerVersion.AutoDetect(Configuration.GetConnectionString("DbConnection"))));
                hangfireConnection = "DbConnection";
            }

            services.AddMvc(options => options.Filters.Add(typeof(AppInitialiserFilter)));

            services.AddIdentity<User, Role>(options => 
            options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<MarkingDbContext>();

            services.AddHttpContextAccessor();

            services.AddHangfire(configuration => configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseSerializerSettings(new JsonSerializerSettings 
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            })
            .UseSqlServerStorage(Configuration.GetConnectionString(hangfireConnection), new SqlServerStorageOptions 
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                DisableGlobalLocks = true
            }));

            services.AddHangfireServer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "marking_api.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
