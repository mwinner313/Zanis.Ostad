using System.Collections.Generic;
using MediatR;
using Zains.Ostad.Application.ExamSamples.Dto;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.ExamSamples.Queries.GetExamSamples
{
    public class GetExamSamplesQuery:IRequest<List<ExamSampleDto>>
    {
        public int GradeId { get; set; }
        public int FieldId { get; set; }
    }
}