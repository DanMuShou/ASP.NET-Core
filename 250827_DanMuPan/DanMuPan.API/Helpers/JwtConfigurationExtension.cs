using System.Text;
using DanMu.Pan.Data.Dto;
using DanMu.Pan.Data.Dto.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace DanMuPan.API.Helpers;

public static class JwtConfigurationExtension
{
    public static void AddJwtConfiguration(this IServiceCollection services, JwtSetting jwtSetting)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // 指定用于身份验证的默认方案为 JwtBearerDefaults.AuthenticationScheme
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // 指定用于质询的默认方案
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSetting.Key)
                    ), // 设置签名密钥
                    ValidateIssuer = true, // 验证发行方
                    ValidIssuer = jwtSetting.Issuer,

                    ValidateAudience = true, // 验证受众
                    ValidAudience = jwtSetting.Audience,

                    ValidateLifetime = true, // 验证令牌有效期
                    ClockSkew = TimeSpan.FromMinutes(jwtSetting.MinutesToExpiration),
                };

                options.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = context =>
                    {
                        if (context.SecurityToken is not JsonWebToken jsonWebToken)
                            return Task.CompletedTask;

                        var userId = jsonWebToken
                            .Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)
                            ?.Value;
                        var email = jsonWebToken
                            .Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Email)
                            ?.Value;
                        context.HttpContext.Items["Id"] = userId; // 将用户ID存储到HTTP上下文项中
                        var userInfoToken =
                            context.HttpContext.RequestServices.GetRequiredService<UserInfoToken>(); // 获取UserInfoToken服务实例并设置用户信息
                        userInfoToken.Id = Guid.Parse(userId);
                        userInfoToken.Email = email;
                        return Task.CompletedTask;
                    }, // 当令牌验证通过时触发
                };
            });
        services.AddAuthorization();
    }
}
