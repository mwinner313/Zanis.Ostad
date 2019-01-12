using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities.AndroidApp;

namespace Zanis.Ostad.Repository.Config
{
    public class AppVersionConfig:IEntityTypeConfiguration<AppVersion>
    {
        public void Configure(EntityTypeBuilder<AppVersion> builder)
        {
            builder.HasMany(x => x.Features).WithOne(x=>x.Version).HasForeignKey(x=>x.VersionId);
        }
    }
}