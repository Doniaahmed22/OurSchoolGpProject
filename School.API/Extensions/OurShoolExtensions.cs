using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using School.Data.Context;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using School.Services.Services.ParentServices;
using School.Services.Services.ProfileServices;
using School.Services.Services.StudentServices;
using System;

namespace School.API.Extensions
{
    public static class OurShoolExtensions
    {
        public static IServiceCollection AddSchoolServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IStudentServices, StudentServices>();
            services.AddScoped<IParentServices, ParentServices>();

            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }

        

    }
    

}
