using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities;

namespace Zanis.Ostad.Repository.Config
{
    public class LessonExamMappingConfig:IEntityTypeConfiguration<LessonExamMapping>
    {
        public void Configure(EntityTypeBuilder<LessonExamMapping> builder)
        {
            builder.HasOne(x => x.Lesson).WithMany(x => x.ExamSamples).HasForeignKey(x=>x.LessonId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.ExamSampleFile).WithMany(x => x.Lessons).HasForeignKey(x=>x.ExamSampleFileId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}