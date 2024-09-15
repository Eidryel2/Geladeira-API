using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Repository.Models;
using Service.Interfaces;
using Service.ServicesClasses;
using System;



namespace Service.DIP
{
    public class Initializer
    {
        public static void Configure(IServiceCollection services, string connection)
        {
            services.AddDbContext<ApiGeladeiraContext>(options => options.UseSqlServer(connection));

            services.AddScoped(typeof(IItemRepository<ItemDomain>), typeof(ItemRepository));
            services.AddScoped(typeof(IServices<ItemDomain>), typeof(ItemServices));
        }
    }
}
