using System.Collections.Generic;
using Zanis.Ostad.Core.Entities.Cart;

namespace Zanis.Ostad.Web.Models
{
    public class DecorData
    {
        public List<DecorDataItem> Items { get; set; }
        public string Title { get; set; }
    }

    public class DecorDataItem
    {
        public string Title { get; set; }
        public int Price { get; set; }
        public int OffPercentage { get; set; }
        public long Id { get; set; }
        public ProductType Type { get; set; }
    }
}
