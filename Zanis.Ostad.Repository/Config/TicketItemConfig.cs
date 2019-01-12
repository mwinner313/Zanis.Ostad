using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zanis.Ostad.Repository.Config
{
    public class TicketItemConfig : IEntityTypeConfiguration<TicketItem>
    {
        public void Configure(EntityTypeBuilder<TicketItem> builder)
        {
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}