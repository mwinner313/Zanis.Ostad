namespace Zains.Ostad.Application.Fields.Queries.GetFieldsList
{
    public class FieldListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int? CollegeId { get; set; }
        public int GradeId { get; set; }
        public string GradeName { get; set; }
    }
}