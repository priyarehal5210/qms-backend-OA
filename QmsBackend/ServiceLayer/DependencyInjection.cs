using DataAccessLayer.Context;
using DataAccessLayer.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ServiceLayer.Jwt;
using ServiceLayer.Mapping;
using ServiceLayer.Services;
using ServiceLayer.Services.IServices;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public static class DependencyInjection
    {
        public static void AddDataContext(this IServiceCollection  services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("conStr"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddTransient<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwagger>();
            services.AddScoped<ITaskService, TasksService>();
            services.AddScoped<IAssignTasksService, AssignTasksService>();
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.AddScoped<IUserSuccessService, UserSuccessService>();
            services.AddAutoMapper(typeof(ProfileMapper));

        }
    }
}
