using System.Collections.Generic;
using MediatR;

namespace Zains.Ostad.Application.General.SearchInFieldsAndLessons
{
    public class SearchInFieldsAndLessonsQuery:IRequest<List<SearchResult>>
    {
        public string SearchText { get; set; }
    }
}