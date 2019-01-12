using System.Collections.Generic;
using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities.AndroidApp
{
    public class AppVersion:BaseEntity<int>
    {
        public ICollection<AppVersionFeature> Features { get; set; }
        public string Version { get; set; }
        public bool IsRequired { get; set; }
    }
}