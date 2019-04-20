using System.Collections.Generic;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities
{
    public class College: BaseEntity<int>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string IconPath { get; set; }
        public string PermaLink { get; set; }
        public ICollection<Field> Fields { get; set; }
    }
}