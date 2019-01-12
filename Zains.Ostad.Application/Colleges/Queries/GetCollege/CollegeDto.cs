using System.Collections.Generic;
using Zains.Ostad.Application.Fields.Queries.GetFieldsList;

namespace Zains.Ostad.Application.Colleges.Queries.GetCollege
{
    public class CollegeDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CoverPath { get; set; }
        public ICollection<FieldListViewModel> Fields { get; set; }
    }
}