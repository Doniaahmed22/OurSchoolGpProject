using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Data.Entities.ChatHub;
using School.Data.Entities.Identity;
using School.Data.Entities.ProgressReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace School.Data.Context
{
    public class SchoolDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles").HasKey(r => r.Id);

            modelBuilder.Entity<Subject>()
            .Property(s => s.Image)
            .HasDefaultValue("Default.jpeg");
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Student>().HasOne(x => x.Class).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Student>().HasOne(x => x.Department).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Student>().HasOne(x => x.Level).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Class>().HasOne(x => x.Department).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Class>().HasOne(x => x.Level).WithMany().OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany()
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<StudentSubject>().HasKey(x => new { x.StudentId, x.SubjectId });
            modelBuilder.Entity<Attendance>().HasKey(x => new { x.StudentId, x.Date });
            modelBuilder.Entity<TeacherSubject>().HasKey(x => new { x.TeacherId, x.SubjectId });
            modelBuilder.Entity<ClassTeacherSubjectDto>().HasKey(x => x.Id);
            modelBuilder.Entity<SubjectLevelDepartmentTerm>().HasKey(x => x.Id);
            modelBuilder.Entity<ClassMaterial>().HasKey(x => new { x.MaterialId, x.ClassId });
            modelBuilder.Entity<AbsenceWarning>().HasKey(x => new { x.StudentId, x.WarningDate });
            modelBuilder.Entity<ProgressReport>().HasKey(x => new { x.StudentId, x.SubjectId });
            modelBuilder.Entity<RequestMeeting>().HasKey(x => new { x.StudentId, x.Date });

            modelBuilder.Entity<SchoolInfo>()
                    .Property(e => e.CurrentTerm)
                             .HasDefaultValue(1);

            modelBuilder.Entity<SchoolInfo>()
                    .Property(e => e.Midterm)
                    .HasDefaultValue(20);

            modelBuilder.Entity<SchoolInfo>()
            .Property(e => e.Workyear)
                 .HasDefaultValue(20);

            modelBuilder.Entity<SchoolInfo>()
                  .Property(e => e.FinalDegree)
                  .HasDefaultValue(60);

            modelBuilder.Entity<AnnouncementClass>()
            .HasKey(ac => new { ac.AnnouncementId, ac.ClassId });

            modelBuilder.Entity<AnnouncementClass>()
                .HasOne(ac => ac.Announcement)
                .WithMany(a => a.AnnouncementClasses)
                .HasForeignKey(ac => ac.AnnouncementId);

            //modelBuilder.Entity<Student>().HasOne(s => s.AppUser).WithOne().HasForeignKey<Student>(s => s.UserId);
            //modelBuilder.Entity<Parent>().HasOne(s => s.AppUser).WithOne().HasForeignKey<Parent>(s => s.UserId);
            //modelBuilder.Entity<Teacher>().HasOne(s => s.AppUser).WithOne().HasForeignKey<Teacher>(s => s.UserId);


        }
        
        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<SubjectLevelDepartmentTerm> SubjectLevelDepartmentTerms { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Attendance> Attendences { get; set; }
        public DbSet<SchoolInfo> SchoolInfo { get; set; }
        public DbSet<TeacherSubject> TeacherSubjects { get; set; }
        public DbSet<ClassTeacherSubjectDto> TeacherSubjectClasses { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ClassMaterial> ClassMaterials { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<AnnouncementClass> AnnouncementClasses { get; set; }

        public DbSet<AbsenceWarning> AbsenceWarnings { get; set; }      
        public DbSet<ProgressReport> ProgressReport { get; set; }
        public DbSet<RequestMeeting> requestMeetings { get; set; }
        public DbSet<Message>  Messages { get; set; }
        public DbSet<ActiveUserConnection> ActiveUserConnections { get; set; }

    }
}
