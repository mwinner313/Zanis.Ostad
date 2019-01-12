using System.Collections.Generic;

namespace Zains.Ostad.Application.General.SearchInFieldsAndLessons
{
    public class SearchResult
    {
        public object Id { get; set; }
        public string Title { get; set; }
        public string Grade { get; set; }
        public List<string> Fields { get; set; }
        public SearchItemType Type { get; set; }
    }
}