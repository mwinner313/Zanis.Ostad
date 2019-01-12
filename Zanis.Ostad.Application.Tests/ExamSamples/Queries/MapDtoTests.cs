using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;
using Zains.Ostad.Application.ExamSamples.Dto;
using Zanis.Ostad.Core.Entities;

namespace Zanis.Ostad.Application.Tests.ExamSamples.Queries
{
    public class MapDtoTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public MapDtoTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShowTitleCorrect()
        {
            _testOutputHelper.WriteLine(new ExamSampleDto
            {
                Type = ExamSampleFileType.AnatomicalAnswer,
                SemesterTitle = "97-98"
            }.Title);
            _testOutputHelper.WriteLine(new ExamSampleDto
            {
                Type = ExamSampleFileType.TestAnswer,
                SemesterTitle = "97-98"
            }.Title);
            _testOutputHelper.WriteLine(new ExamSampleDto
            {
                Type = ExamSampleFileType.Exam,
                SemesterTitle = "97-98"
            }.Title);
        }
    }
}