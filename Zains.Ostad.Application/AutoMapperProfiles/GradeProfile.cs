using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
using Zains.Ostad.Application.Grades.Queries.GetGrades;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.AutoMapperProfiles
{
    public class GradeProfile:Profile
    {
        public GradeProfile()
        {
            CreateMap<Grade, GradeViewModel>();
        }
    }
}