using System.Collections.Generic;

namespace Zains.Ostad.Application.AndroidAppVersions.Dtos
{
    public class AppVersionViewModel
    {
        public List<AppVersionFeatureViewModel> Features { get; set; }
        public string Version { get; set; }
        public bool IsRequired { get; set; }
    }
}