using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities;

namespace Zanis.Ostad.Repository.Config
{
    public class ExamSampleFileConfig:IEntityTypeConfiguration<ExamSampleFile>
    {
        public void Configure(EntityTypeBuilder<ExamSampleFile> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}