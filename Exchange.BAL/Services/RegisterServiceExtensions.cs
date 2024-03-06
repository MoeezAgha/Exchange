using Exchange.BAL.Services.AutoMapper;
using Exchange.BAL.Services.Contracts;
using Exchange.BAL.Services.Repositories;
using Exchange.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
        
namespace Exchange.BAL.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRegisterBusinessServices(this IServiceCollection services)
        {
          //services.AddAutoMapper(typeof(AutoMapperProfiles));
          services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
         services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            


            //services.AddIdentity<ApplicationUser, IdentityRole<int>>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>()
            //    .AddDefaultTokenProviders();

            return services;
        }
    }
}
