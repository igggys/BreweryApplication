using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceSecurityServer.DataLayer;
using ServiceSecurityServer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSecurityServer
{
    public static class ServiceSecurityServerExtensions
    {
        public static void AddServiceSecurityServer(this IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IdentityDataManagerSettingsReader>();
            services.AddSingleton<IdentityDataManager>();

        }
    }
}
