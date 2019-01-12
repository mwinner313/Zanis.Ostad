using Zanis.Ostad.Core.Infrastucture;

namespace Zanis.Ostad.Core.Entities.AndroidApp
{
    public class AppVersionFeature:BaseEntity<int>
    {
        public string Title { get; set; }
        public AppVersion Version { get; set; }
        public int VersionId { get; set; }
    }
}