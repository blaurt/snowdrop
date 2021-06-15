using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Snowdrop.Auth.Managers.TokenStorage;
using Snowdrop.Auth.Models.Configuration;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace Snowdrop.Auth.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSnowdropJwt(this IServiceCollection collection, IConfiguration configuration)
        {
            var config = configuration.GetSection(nameof(JwtConfig)).Get<JwtConfig>();
            if (config == null)
            {
                throw new ArgumentException(nameof(JwtConfig));
            }

            collection.AddSingleton(config);
            collection.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = config.Issuer,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.Secret)),
                        ValidAudience = config.Audience,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromSeconds(30)
                    };
                });
        }

        public static void AddMemorySessionManager(this IServiceCollection collection, IConfiguration configuration)
        {
            if (collection.Any(s => s.ServiceType == typeof(ITokenStorage)))
            {
                throw new ArgumentException($"Service for {nameof(ITokenStorage)} has already been added");
            }

            collection.AddSingleton(typeof(ITokenStorage), typeof(RedisTokenStorage));
            var redisConfiguration = configuration.GetSection("Redis").Get<RedisConfiguration>();
            collection.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(redisConfiguration);
        }
    }
}