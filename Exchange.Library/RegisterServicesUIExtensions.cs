
using Exchange.UI.Library.Helper;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.UI.Library
{


    public static class RegisterServicesUIExtensions
    {
        public static IServiceCollection AddRegisterServicesUIExtensions(this IServiceCollection services)
        {
           

            services.AddAutoMapper(typeof(AutoMapperProfiles));


         

            return services;
        }
    }
}
