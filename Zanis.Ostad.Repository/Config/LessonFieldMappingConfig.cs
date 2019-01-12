using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities;

namespace Zanis.Ostad.Repository.Config
{
    public class LessonFieldMappingConfig:IEntityTypeConfiguration<LessonFieldMapping>
    {
        public void Configure(EntityTypeBuilder<LessonFieldMapping> builder)
        {
            builder.HasOne(x=>x.Lesson).WithMany(x=>x.Fields).HasForeignKey(x=>x.LessonId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x=>x.Field).WithMany(x=>x.FieldLessons).HasForeignKey(x=>x.FieldId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x=>x.Grade).WithMany().HasForeignKey(x=>x.GradeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}