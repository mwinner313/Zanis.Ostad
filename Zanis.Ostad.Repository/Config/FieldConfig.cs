using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities;

namespace Zanis.Ostad.Repository.Config
{
    public class FieldConfig : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.HasMany(x => x.FieldLessons).WithOne(x => x.Field).HasForeignKey(x => x.FieldId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Grade).WithMany(x => x.Fields).HasForeignKey(x => x.GradeId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}