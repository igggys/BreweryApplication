using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CryptoLib
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddCrypto(this IServiceCollection services)
        {
            services.AddSingleton<AsymmetricEncryption>();
            services.AddSingleton<SymmetricEncryption>();
        }
    }
}
