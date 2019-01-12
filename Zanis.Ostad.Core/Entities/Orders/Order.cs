using System.Collections.Generic;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities.Orders
{
    public class Order:BaseEntity<long>
    {
        public List<OrderItem> OrderItems { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
        public string ExtraData { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}