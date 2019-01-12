using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities;

namespace Zanis.Ostad.Repository.Config
{
    public class StudentContentGroupMappingConfig:IEntityTypeConfiguration<StudentCourseMapping>
    {
        public void Configure(EntityTypeBuilder<StudentCourseMapping> builder)
        {
            builder.HasOne(x => x.Student).WithMany(x => x.BoughtCourses).HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Course).WithMany(x => x.Students).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}