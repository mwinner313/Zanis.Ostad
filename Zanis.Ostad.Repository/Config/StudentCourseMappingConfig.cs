using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities;

namespace Zanis.Ostad.Repository.Config
{
    public class StudentCourseMappingConfig:IEntityTypeConfiguration<StudentCourseMapping>
    {
        public void Configure(EntityTypeBuilder<StudentCourseMapping> builder)
        {
          builder.HasOne(x => x.Course).WithMany(x=>x.Students).HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}