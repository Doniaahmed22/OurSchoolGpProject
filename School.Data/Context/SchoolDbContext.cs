using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace School.Data.Context
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Student>().HasOne(x => x.Class).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Student>().HasOne(x => x.Department).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Student>().HasOne(x => x.Level).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Class>().HasOne(x => x.Department).WithMany().OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Class>().HasOne(x => x.Level).WithMany().OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<StudentSubject>().HasKey( x => new {x.StudentId , x.SubjectId});
            modelBuilder.Entity<Attendence>().HasKey( x => new {x.StudentId , x.TeacherId});
            modelBuilder.Entity<TeacherLevel>().HasKey( x => new {x.LevelId , x.TeacherId});
            modelBuilder.Entity<TeacherSubjectLevel>().HasKey( x => new {x.LevelId , x.TeacherId , x.SubjectId});
            modelBuilder.Entity<TeacherSubjectClass>().HasKey( x => new {x.ClassId , x.TeacherId , x.SubjectId});
            modelBuilder.Entity<SubjectLevelDepartmentTerm>().HasKey(x =>x.Id);

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<SubjectLevelDepartmentTerm>SubjectLevelDepartmentTerms { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Attendence> Attendences { get; set; }
        public DbSet<TeacherLevel> TeacherLevels { get; set; }
        public DbSet<SchoolInfo> SchoolInfo { get; set; }
        public DbSet<TeacherSubjectLevel> TeacherSubjectLevels { get; set; }
        public DbSet<TeacherSubjectClass> TeacherSubjectClasses { get; set; }
    }
}
