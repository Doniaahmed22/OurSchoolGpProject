using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using School.Data.Context;
using School.Repository.Interfaces;
using School.Repository.Repositories;
using School.Services.Services.ClassServices;
using School.Services.Services.FileService;
using School.Services.Services.GradeService;
using School.Services.Services.ParentServices;
using School.Services.Services.ProfileServices;
using School.Services.Services.StudentServices;
using School.Services.Services.SubjectRecord;
using School.Services.Services.SubjectServices;
using School.Services.Services.TeacherServices;
using School.Services.Services.FileService;

using System;
using School.Services.Services.MaterialService;
using School.Services.Services.DepartmentService;
using School.Services.Services.LevelService;
using School.Services.Services.TermService;
using School.Services.Tokens;
using School.Services.UserService;
using School.Services.EmailServices;
using School.Services.Services.AnnouncementService;
using School.Services.Services.AttendanceService;
using School.Services.Services.ProgressReportService;
using School.Services.Services.RequestMeetingService;
using School.Services.Services.ChatService;
using School.Repository.Interfaces.ChatHub;
using School.Repository.Repositories.ChatHub;

namespace School.API.Extensions
{
    public static class OurShoolExtensions
    {
        public static IServiceCollection AddSchoolServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
            services.AddScoped<ISubjectRepository, SubjectRepository>();
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();

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
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IMaterialService, MaterialService>();
            services.AddScoped<IdepartmentService, DepartmentService>();
            services.AddScoped<ILevelService, LevelService>();
            services.AddScoped<Services.Services.TermService.ITermService, TermService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            //services.AddTransient<IGetToken, GetToken>();

            services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
            services.AddScoped<IAnnouncementService, AnnouncementService>();


            services.AddScoped<IParentRepository, ParentRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();

            services.AddScoped<IProgressReportService, ProgressReportService>();
            services.AddScoped<IProgressReportRepository, ProgressReportRepository>();
            services.AddScoped<IRequestMeetingRepository, RequestMeetingRepository>();
            services.AddScoped<IRequestMeetingService, RequestMeetingService>();
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IActiveUserRepository , ActiveUserRepository>();
            services.AddAutoMapper(typeof(MappingProfile));

            return services;
        }

        

    }
    

}
