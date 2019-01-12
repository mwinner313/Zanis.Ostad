using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities;

namespace Zanis.Ostad.Repository.Config
{
    public class StudentExamSampleMappingConfig:IEntityTypeConfiguration<StudentExamSampleMapping>
    {
        public void Configure(EntityTypeBuilder<StudentExamSampleMapping> builder)
        {
            builder.HasOne(x => x.Lesson).WithMany().HasForeignKey(x => x.LessonId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Student).WithMany(x => x.BoughtExamSamples).HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}