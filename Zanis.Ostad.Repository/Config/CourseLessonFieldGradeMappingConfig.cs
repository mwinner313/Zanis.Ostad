using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zanis.Ostad.Repository.Config
{
    public class CourseLessonFieldGradeMappingConfig:IEntityTypeConfiguration<CourseLessonFieldGradeMapping>
    {
        public void Configure(EntityTypeBuilder<CourseLessonFieldGradeMapping> builder)
        {
            builder.HasOne(x => x.Course).WithMany(x => x.Lessons).HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Lesson).WithMany(x => x.Courses).HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}