using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zanis.Ostad.Repository.Config
{
    public class CourseItemConfig:IEntityTypeConfiguration<CourseItem>
    {
        public void Configure(EntityTypeBuilder<CourseItem> builder)
        {
            builder.HasMany(x => x.Edits)
                .WithOne(x=>x.CourseItem)
                .HasForeignKey(x => x.CourseItemId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}