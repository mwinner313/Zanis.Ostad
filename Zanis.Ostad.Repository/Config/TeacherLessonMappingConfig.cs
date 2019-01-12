using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zanis.Ostad.Repository.Config
{
    public class TeacherLessonMappingConfig:IEntityTypeConfiguration<TeacherLessonMapping>
    {
        public void Configure(EntityTypeBuilder<TeacherLessonMapping> builder)
        {
            builder.HasOne(x => x.LessonFieldMapping).WithMany(x => x.TeacherLessonMappings).HasForeignKey(x => x.LessonId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}