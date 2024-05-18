﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using School.Data.Context;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using School.Services.Services.ClassServices;
using School.Services.Services.GradeService;
using School.Services.Services.ParentServices;
using School.Services.Services.ProfileServices;
using School.Services.Services.StudentServices;
using School.Services.Services.SubjectRecord;
using School.Services.Services.SubjectServices;
using School.Services.Services.TeacherServices;
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
            services.AddScoped<ITeacherServices, TeacherServices>();
 //           services.AddScoped<ISubjectServices, SubjectServices>();
            services.AddScoped<ISubjectServices, SubjectServices>();
            services.AddScoped<IGradeServices, GradeServices>();

            services.AddScoped<ISubjectRecordServices, SubjectRecordServices>();
            services.AddScoped<ISubjectRecordRepository, SubjectRecordRepository>();
            services.AddScoped<IClassServices, ClassServices>();
 //           services.AddScoped<IClassRecordServices, ClassRecordServices>();
            services.AddScoped<IGradeRepository, GradeRepository>();

            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }

        

    }
    

}
