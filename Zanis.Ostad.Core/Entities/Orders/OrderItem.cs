using Zanis.Ostad.Core.Entities.Cart;
using Zanis.Ostad.Core.Infrastucture;
namespace Zanis.Ostad.Core.Entities.Orders
{
    public class OrderItem:BaseEntity<long>
    {
        public int Price { get; set; }
        public long ProductId { get; set; }
        public ProductType ProductType { get; set; }
    }
}