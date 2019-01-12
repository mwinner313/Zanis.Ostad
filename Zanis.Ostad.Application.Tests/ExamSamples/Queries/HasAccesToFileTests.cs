using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MockQueryable.Moq;
using Moq;
using Xunit;
using Zains.Ostad.Application.ExamSamples.Queries.HasAccessToFile;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities;

namespace Zanis.Ostad.Application.Tests.ExamSamples.Queries
{
    public class HasAccesToFileTests
    {
        private readonly Mock<IRepository<StudentExamSampleMapping, long>> _studentBoughtItems;
        private readonly Mock<IRepository<ExamSampleFile, int>> _examRepository;

        public HasAccesToFileTests()
        {
            _studentBoughtItems = new Mock<IRepository<StudentExamSampleMapping, long>>();
            _examRepository = new Mock<IRepository<ExamSampleFile, int>>();
        }

        [Fact]
        public async Task ShouldSayUnKnownIfFileDoesNotExists()
        {
            var filePath = "path-to-some-secured-file";
            _examRepository.Setup(x => x.GetQueriable())
                .Returns(new List<ExamSampleFile>().AsQueryable().BuildMock().Object);
            var handler = new HasAccessToFileQueryHandler(_studentBoughtItems.Object, _examRepository.Object);
            var res = await handler.Handle(new HasAccessToFileQuery(0, filePath), CancellationToken.None);
            Assert.Equal(res.Status, ResponseStatus.UnKnown);
        }

        [Fact]
        public async Task ShouldAllowBoughtItemForUser()
        {
            var filePath = "path-to-some-secured-file";
            long studentId = 2;
            _examRepository.Setup(x => x.GetQueriable())
                .Returns(new List<ExamSampleFile>
                {
                    new ExamSampleFile()
                    {
                        FilePath = filePath
                    }
                }.AsQueryable().BuildMock().Object);
            _studentBoughtItems.Setup(x => x.GetQueriable()).Returns(new List<StudentExamSampleMapping>
            {
                new StudentExamSampleMapping
                {
                    StudentId = studentId,
                    Lesson = new Lesson
                    {
                        ExamSamples = new List<LessonExamMapping>
                        {
                            new LessonExamMapping
                            {
                               ExamSampleFile = new ExamSampleFile()
                               {
                                   FilePath = filePath
                               }
                            }
                        }
                    }
                }
            }.AsQueryable().BuildMock().Object);
            var handler = new HasAccessToFileQueryHandler(_studentBoughtItems.Object, _examRepository.Object);
            var res = await handler.Handle(new HasAccessToFileQuery(studentId, filePath), CancellationToken.None);
            Assert.Equal(res.Status, ResponseStatus.Success);
        }
        [Fact]
        public async Task ShouldNotAllowNotBoughtItemForUser()
        {
            var filePath = "path-to-some-secured-file";
            long studentId = 2;
            _examRepository.Setup(x => x.GetQueriable())
                .Returns(new List<ExamSampleFile>
                {
                    new ExamSampleFile
                    {
                        FilePath = filePath
                    }
                }.AsQueryable().BuildMock().Object);
            _studentBoughtItems.Setup(x => x.GetQueriable()).Returns(new List<StudentExamSampleMapping>().AsQueryable().BuildMock().Object);
            var handler = new HasAccessToFileQueryHandler(_studentBoughtItems.Object, _examRepository.Object);
            var res = await handler.Handle(new HasAccessToFileQuery(studentId, filePath), CancellationToken.None);
            Assert.Equal(res.Status, ResponseStatus.Fail);
        }
    }
}