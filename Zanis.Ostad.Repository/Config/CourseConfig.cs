using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zanis.Ostad.Repository.Config
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasOne(x => x.CourseCategory).WithMany(x => x.Courses)
                .HasForeignKey(x => x.CourseCategoryId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.Contents)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);
          
            builder.HasMany(x => x.Students)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId);
            builder.HasMany(x => x.Lessons)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}