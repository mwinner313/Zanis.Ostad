using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.AndroidApp;
using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Entities.Notifications;
using Zanis.Ostad.Core.Entities.Orders;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zanis.Ostad.Repository
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, long>
    {
        public ApplicationDbContext(DbContextOptions opts) : base(opts)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            base.OnModelCreating(builder);
        }

        public ApplicationDbContext() : base(GetOptions())
        {
        }

        private static DbContextOptions GetOptions()
        {
            
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer("Server=212.86.67.131 ;User Id=mqanbari; Password=13Magh75; Database=ostad_zanis;");
            // builder.UseSqlServer("Server=. ;Integrated Security=true; Database=ostad_zanis;");
            return builder.Options;
        }

        public DbSet<CourseItem> Contents { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseTitle> CourseTitles { get; set; }
        public DbSet<TeacherLessonMapping> TeacherLessonMappings { get; set; }
        public DbSet<StudentCourseMapping> StudentCourseMappings { get; set; }
        public DbSet<StudentExamSampleMapping> StudentExamSampleMappings { get; set; }
        public DbSet<College> Colleges { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonFieldMapping> LessonFieldMappings { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketItem> TicketItems { get; set; }
        public DbSet<ExamSampleFile> ExamSampleFiles { get; set; }
        public DbSet<LessonExamMapping> LessonExamMappings { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<AppVersion> AppVersions { get; set; }
        public DbSet<AppVersionFeature> AppVersionFeatures { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}