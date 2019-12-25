using System.Text;
using Galaxy.Api.Core.Interfaces;
using Galaxy.Api.Core.Services;
using Galaxy.Api.Core.Services.Users;
using Galaxy.Teams.Core.Models.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Galaxy.Api.Presentation.Ioc
{
    public static class RegisterAuthentication
    {
        public static IServiceCollection AddUserAuthentication(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            var builder = services.BuildServiceProvider();
            var appSettings = builder.GetService<IOptions<AppSettings>>();
            
            
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.IncludeErrorDetails = true;
                    options.RequireHttpsMetadata = true;
                    options.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async ctx =>
                        {
                            await ctx.HttpContext.RequestServices.GetRequiredService<IUserService>()
                                .ValidateAsync(ctx);
                        }
                    };
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = JwtRegisteredClaimNames.Jti,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = appSettings.Value.Jwt.Issuer,
                        ValidAudience = appSettings.Value.Jwt.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(appSettings.Value.Jwt.Key))
                    };
                });
            return services;
        }

    }
}