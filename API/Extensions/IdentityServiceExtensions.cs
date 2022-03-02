using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Infrastructure.Data;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static WebApplicationBuilder AddIdentityService(this WebApplicationBuilder builder)
        {
           var identityBuilder = builder.Services.AddIdentityCore<ApplicationDbUser>();
            identityBuilder = new IdentityBuilder(identityBuilder.UserType,identityBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<ApplicationDbContext>();
            identityBuilder.AddSignInManager<SignInManager<ApplicationDbUser>>();

            // SignInManager relys on authentication service

            builder.Services.AddAuthentication();

            return builder;
        }
    }
}