using Microsoft.EntityFrameworkCore;

namespace Zanis.Ostad.Repository
{
    public class DataDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source=.;initial catalog=a;integrated security=True;MultipleActiveResultSets=True;");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Data> Datas { get; set; }
        public DbSet<Manba> Manbas{ get; set; }
    }

    public class Manba
    {
        public int Id { get; set; }
        public string Lesson { get; set; }
        /// <summary>
        /// lessson code
        /// </summary>
        public double? UniGroupLessonCode{ get; set; }
       /// <summary>
       /// field code
       /// </summary>
        public double? UniGroupFieldCode{ get; set; }
        public double? GradeCode{ get; set; }    
        public string Grade{ get; set; }    
        public string College{ get; set; }    
        public string Field{ get; set; }
        public string GradeUniGroupLessonFieldCode { get; set; }
        public string GradeUniFieldCode { get; set; }
    }

    public class Data
    {
        public double UniversityCode { get; set; }
        public string UniversityTitle { get; set; }
        public double GroupCode { get; set; }
        public double FieldCode { get; set; }
        public string FieldName { get; set; }
        public string Grade { get; set; }
        public string GroupTitle { get; set; }
        public int Id { get; set; }
    }
}