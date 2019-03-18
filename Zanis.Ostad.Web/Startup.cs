using System;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using Zains.Ostad.Application.Colleges.Queries.GetCollegeList;
using Zains.Ostad.Application.Courses;
using Zains.Ostad.Application.Users;
using Zanis.Ostad.Common;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Payment;
using Zanis.Ostad.Payment.Refah;
using Zanis.Ostad.Repository;
using Zanis.Ostad.Web.Infrastracture;

namespace Zanis.Ostad.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureMvc(services);
            services.AddMediatR(typeof(GetCollegeQueryHandler).Assembly);
            services.AddAutoMapper(x =>
            {
                x.AddProfiles(typeof(GetCollegeQueryHandler).Assembly);
            });
            services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
            services.AddScoped<IUnitOfWork,SqlUnitOfWork>();
            services.AddScoped<IWorkContext,WebWorkContext>();
            services.AddScoped<IOrderPaymentProviderFactory,OrderPaymentProviderFactory>();
            services.AddScoped<RefahPaymentProvider,RefahPaymentProvider>();
            services.AddScoped<IEmailService,EmailService>();
            services.AddScoped(sp=> new EmailSenderInfo
            {
                UserName = Configuration["EmailSender:UserName"],
                From = Configuration["EmailSender:From"],
                Host = Configuration["EmailSender:Host"],
                Password = Configuration["EmailSender:Password"],
                Port = int.Parse(Configuration["EmailSender:Port"]),
                IsSSlEnabled = bool.Parse(Configuration["EmailSender:IsSSlEnabled"])
            });
            services.AddCors();
            ConfigureIdentitySystem(services);
            ConfigureBearerTokenAuthentication(services);
            ConfigureSwagger(services);
            services.AddDbContext<ApplicationDbContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString(Configuration["SelectedConnectionString"]),
                    b => { b.MigrationsAssembly("Zanis.Ostad.Repository"); });
            });
        }

        private static void ConfigureIdentitySystem(IServiceCollection services)
        {
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<UserManager<User>, AppUserManager>();
            services.AddScoped<IAppRoleManager, AppRoleManager>();
            services.AddScoped<IUserRoleRepository, EfUserRoleRepository>();
            services.AddScoped<IAppUserManager, AppUserManager>();
            services.AddScoped<ICoursesFileManager, CoursesFileManager>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Tokens.ChangePhoneNumberTokenProvider = "Phone";
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = false;
            });
        }

        private void ConfigureBearerTokenAuthentication(IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["jwt:JwtIssuer"],
                        ValidAudience = Configuration["jwt:JwtIssuer"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:JwtKey"])),
                        RequireExpirationTime = false
                    };
                });
        }

        private static void ConfigureMvc(IServiceCollection services)
        {
            services.AddMvc(x => x.Filters.Add<ModelStateValidationFilter>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(x =>
                {
                    x.RegisterValidatorsFromAssembly(typeof(GetCollegeQuery).Assembly);
                    x.LocalizationEnabled = true;
                    ValidatorOptions.LanguageManager.Culture = new CultureInfo("fa-IR");
                });
        }

        private static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerAuthorizationHeaderParameterOperationFilter>();
                c.SwaggerDoc("v1", new Info {Title = "My API", Version = "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            if (env.IsDevelopment())
            {


                // Webpack initialization with hot-reload.
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseAuthentication();
            app.UseMiddleware<SecureZanisProtectedFilesMiddleware>();
            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new {controller = "Home", action = "Index"});
            });
        }
    }
}
