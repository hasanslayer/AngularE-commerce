using System.Text;
using API.Infrastructure.Data;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static WebApplicationBuilder AddIdentityService(this WebApplicationBuilder builder, IConfiguration config)
        {
            var identityBuilder = builder.Services.AddIdentityCore<ApplicationDbUser>();
            identityBuilder = new IdentityBuilder(identityBuilder.UserType, identityBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<ApplicationDbContext>();
            identityBuilder.AddSignInManager<SignInManager<ApplicationDbUser>>();

            // SignInManager relys on authentication service

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Token:Key"])),
                        ValidIssuer = config["Token:Issuer"],
                        ValidateIssuer = true,
                    };
                });

            return builder;
        }
    }
}