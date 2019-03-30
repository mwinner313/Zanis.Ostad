using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities.Orders;

namespace Zanis.Ostad.Repository.Config
{
    public class OrderConfig:IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasMany(x => x.OrderItems).WithOne().HasForeignKey(x=>x.OrderId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}