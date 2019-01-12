using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.ExamSamples.Dto
{
    public class ExamSampleDto
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public ExamSampleFileType Type { get; set; }
        public string SemesterTitle { get; set; }
        public string Title => GetTitle();

        private string GetTitle()
        {
            var title = "";
            switch (Type)
            {
                    case ExamSampleFileType.Exam:
                        title = "نمونه سوال";
                        break;
                case ExamSampleFileType.AnatomicalAnswer:
                    title = "پاسخ تشریحی نمونه سوال";
                    break;
                case ExamSampleFileType.TestAnswer:
                    title = "پاسخ تستی نمونه سوال";
                    break;
            }

            return title + " " + SemesterTitle;
        }
    }
}