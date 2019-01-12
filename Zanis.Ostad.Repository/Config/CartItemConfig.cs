using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zanis.Ostad.Core.Entities.Cart;

namespace Zanis.Ostad.Repository.Config
{
    public class CartItemConfig:IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasOne(x => x.User).WithMany(x => x.Cart).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Course).WithMany().HasForeignKey(x => x.CourseId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.LessonExam).WithMany().HasForeignKey(x => x.LessonExamId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}