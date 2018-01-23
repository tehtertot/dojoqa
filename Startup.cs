using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dojoQA.Auth;
using dojoQA.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace dojoQA
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //DB settings - postgres
            services.AddEntityFrameworkNpgsql().AddDbContext<DojOverflowContext>(options => options.UseNpgsql(Configuration["DBInfo:ConnectionString"]));

            //add identity services
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<DojOverflowContext>()
                    .AddDefaultTokenProviders();

            //configure identity services with password settings, lockout options, unique email
            // services.Configure<IdentityOptions>(options =>
            // {
            //     // Password settings -- set on frontend?
            //     options.Password.RequireDigit = true;
            //     options.Password.RequiredLength = 8;
            //     options.Password.RequireNonAlphanumeric = false;
            //     options.Password.RequireUppercase = true;
            //     options.Password.RequireLowercase = false;
            //     options.Password.RequiredUniqueChars = 6;

            //     // Lockout settings
            //     options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            //     options.Lockout.MaxFailedAccessAttempts = 10;
            //     options.Lockout.AllowedForNewUsers = true;

            //     // User settings
            //     options.User.RequireUniqueEmail = true;
            // });
            services.AddIdentityCore<ApplicationUser>(options => 
            {                
                options.User.RequireUniqueEmail = true;
            });

            //cookie settings
            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
            });
            
            //JWT issuer settings
            services.AddSingleton<JwtFactory>();
            var jwtAppSettingsOptions = Configuration.GetSection("JwtIssuerOptions");

            services.Configure<JwtIssuerOptions>(options => {
                options.Issuer = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
            ValidateIssuer = true,
            ValidIssuer = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Issuer)],

            ValidateAudience = true,
            ValidAudience = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Audience)],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = _signingKey,

            RequireExpirationTime = false,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingsOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Helpers.Constants.Strings.JwtClaimIdentifiers.Rol, Helpers.Constants.Strings.JwtClaims.ApiAccess));
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
