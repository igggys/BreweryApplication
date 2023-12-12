using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceSecurityServer.DataLayer;
using ServiceSecurityServer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSecurityClient
{
    public static class ServiceSecurityClientExtensions
    {
        public static void AddServiceSecurityClient(this IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IdentityDataManagerSettingsReader>();
            services.AddSingleton<IdentityDataManager>();

        }
    }
}
