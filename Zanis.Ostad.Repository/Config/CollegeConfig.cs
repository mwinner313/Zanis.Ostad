using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities;

namespace Zanis.Ostad.Repository.Config
{
    public class CollegeConfig:IEntityTypeConfiguration<College>
    {
        public void Configure(EntityTypeBuilder<College> builder)
        {
            builder.HasMany(x => x.Fields).WithOne(x => x.College).HasForeignKey(x => x.CollegeId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}