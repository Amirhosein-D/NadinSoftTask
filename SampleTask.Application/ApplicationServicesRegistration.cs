using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SampleTask.Application.Contracts.Presistence;
using SampleTask.Application.Tools.Captcha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleTask.Application
{
    public static class ApplicationServicesRegistration
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ICaptchaTools, CaptchaTools>();
        }




    }
}
