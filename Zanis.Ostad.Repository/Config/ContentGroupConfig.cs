using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zanis.Ostad.Repository.Config
{
    public class ContentGroupConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasOne(x => x.CourseTitle).WithMany(x => x.Courses)
                .HasForeignKey(x => x.CourseTitleId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Contents)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
          
            builder.HasMany(x => x.Students)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId);
            builder.HasOne(x => x.TeacherLessonMapping).WithMany(x => x.Courses)
                .HasForeignKey(x => x.TeacherLessonMappingId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}