using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyContosoUniversity.Models.ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public class SchoolContext : DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory= LoggerFactory.Create(builder =>
        {
            builder
                    .AddFilter((category, level) =>
                        (category == DbLoggerCategory.Database.Command.Name || category == DbLoggerCategory.ChangeTracking.Name)
                        && level == LogLevel.Debug)
                    .AddDebug();
        });

        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<MyContosoUniversity.Models.ContosoUniversity.Models.Student> Students { get; set; }
        public DbSet<MyContosoUniversity.Models.ContosoUniversity.Models.Course> Courses { get; set; }
        public DbSet<MyContosoUniversity.Models.ContosoUniversity.Models.Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder
        .UseLoggerFactory(MyLoggerFactory) // Warning: Do not create a new ILoggerFactory instance each time
        .UseSqlServer(
            @"Server=(localdb)\mssqllocaldb;Database=SchoolContext-b1fc351d-ec67-4fb7-bc5e-386b99a5e31a;Trusted_Connection=True;ConnectRetryCount=0");


    }
}
