using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MockQueryable.Moq;
using Moq;
using Xunit;
using Zains.Ostad.Application.Courses.Queries.HasAccessToFile;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zanis.Ostad.Application.Tests.Courses.Queries
{
    public class HasAccessToFileTests
    {
        private readonly Mock<IRepository<StudentCourseMapping, long>> _studentCourseRepository;
        private readonly Mock<IRepository<CourseItem, long>> _courseItemRepository;
        private readonly HasAccessToFileQueryHandler _handler;

        public HasAccessToFileTests()
        {
            _courseItemRepository = new Mock<IRepository<CourseItem, long>>();
            _studentCourseRepository = new Mock<IRepository<StudentCourseMapping, long>>();
            _handler = new HasAccessToFileQueryHandler(_courseItemRepository.Object, _studentCourseRepository.Object);
        }

        [Fact]
        public async Task ShouldSayUnKnownIfFileDoesNotExists()
        {
            _courseItemRepository.Setup(x => x.GetQueryable())
                .Returns(new List<CourseItem>().AsQueryable().BuildMock().Object);

            var res = await _handler.Handle(new HasAccessToFileQuery(0, "non-existing-file-path"),
                CancellationToken.None);
            Assert.Equal(res.Status, ResponseStatus.UnKnown);
        }

        [Fact]
        public async Task ShouldAllowBoughtItemForUser()
        {
            var filePath = "path-to-some-secured-file";
            long studentId = 2;
            _courseItemRepository.Setup(x => x.GetQueryable())
                .Returns(new List<CourseItem>
                {
                    new CourseItem
                    {
                        FilePath = filePath,
                    }
                }.AsQueryable().BuildMock().Object);
            _studentCourseRepository.Setup(x => x.GetQueryable()).Returns(new List<StudentCourseMapping>()
            {
                new StudentCourseMapping
                {
                    StudentId = studentId,
                    Course = new Course
                    {
                        Contents = new List<CourseItem>
                        {
                            new CourseItem
                            {
                                FilePath = filePath
                            }
                        }
                    }
                }
            }.AsQueryable().BuildMock().Object);
            var res = await _handler.Handle(new HasAccessToFileQuery(studentId, filePath), CancellationToken.None);
            Assert.Equal(res.Status, ResponseStatus.Success);
        }
        [Fact]
        public async Task ShouldNotAllowNotBoughtItemForUser()
        {
            var filePath = "path-to-some-secured-file";
            long studentId = 2;
            _courseItemRepository.Setup(x => x.GetQueryable())
                .Returns(new List<CourseItem>
                {
                    new CourseItem
                    {
                        FilePath = filePath,
                    }
                }.AsQueryable().BuildMock().Object);
            _studentCourseRepository.Setup(x => x.GetQueryable()).Returns(new List<StudentCourseMapping>().AsQueryable().BuildMock().Object);
            var res = await _handler.Handle(new HasAccessToFileQuery(studentId, filePath), CancellationToken.None);
            Assert.Equal(res.Status, ResponseStatus.Fail);
        }
        [Fact]
        public async Task ShouldAllowIfItemIsPreview()
        {
            var filePath = "path-to-some-secured-file";
            long studentId = 2;
            _courseItemRepository.Setup(x => x.GetQueryable())
                .Returns(new List<CourseItem>
                {
                    new CourseItem
                    {
                        FilePath = filePath,
                        IsPreview = true,
                    }
                }.AsQueryable().BuildMock().Object);
            _studentCourseRepository.Setup(x => x.GetQueryable()).Returns(new List<StudentCourseMapping>().AsQueryable().BuildMock().Object);
            var res = await _handler.Handle(new HasAccessToFileQuery(studentId, filePath), CancellationToken.None);
            Assert.Equal(res.Status, ResponseStatus.Success);
        }
    }
}