using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleTask.Application.Contracts.Persistence;
using SampleTask.Application.Contracts.Presistence;
using SampleTask.Domain;
using SampleTask.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Persistence
{
    public static class PersistenceServicesRegistration
    {

        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SampleTaskDbContext>(options =>
            {

                options.UseSqlServer(configuration.GetConnectionString("SampleTaskConnectionString"));
                //options.UseSqlServer("Data Source = LAPTOP - 8KB21INC; Initial Catalog = CVDb; Integrated Security = True");

            });



            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<SampleTaskDbContext>().AddDefaultTokenProviders();


            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            

            return services;
        }

    }
}
