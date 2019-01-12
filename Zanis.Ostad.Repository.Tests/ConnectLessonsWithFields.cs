//using System;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore.Internal;
//using Xunit;
//using Zanis.Ostad.Core.Entities;
//
//namespace Zanis.Ostad.Repository.Tests
//{
//    public class ConnectLessonsWithFields
//    {
//        [Fact]
//        public async Task Run()
//        {
//            var db = new ApplicationDbContext();
//            var insertedLessons = db.LessonFieldMappings.GroupBy(x => x.Lesson.LessonCode).Select(x => x.Key).ToList();
//            var lessons = db.Lessons.GroupBy(x => x.LessonCode).Where(x=>!insertedLessons.Contains(x.Key)).ToList();
//            foreach (var lesson in lessons)
//            {
//                var fields = db.Fields.Where(x => x.Lessons.Any(l => l.LessonCode == lesson.Key)).ToList();
//                foreach (var field in fields)
//                {
//                    if(!db.LessonFieldMappings.Any(x=>x.FieldId==field.Id && x.LessonId==lesson.First().Id))
//                    db.LessonFieldMappings.Add(new LessonFieldMapping(){FieldId = field.Id,LessonId = lesson.First().Id});
//                }
//
//                try
//                {
//                    await   db.SaveChangesAsync();
//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine(e);
//                    throw;
//                }
//            }
//
//         
//        }
//        [Fact]
//        public async Task DeleteDulicateLessons()
//        {
//            var db = new ApplicationDbContext();
//      //      db.Lessons.Where(x=>!x.Fields.Any()).ToList().
//        }
//    }
//}