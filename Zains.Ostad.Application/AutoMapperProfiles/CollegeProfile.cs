using AutoMapper;
using Zains.Ostad.Application.Colleges.Queries.GetCollege;
using Zains.Ostad.Application.Colleges.Queries.GetCollegeList;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.AutoMapperProfiles
{
    public class CollegeProfile:Profile
    {
        public CollegeProfile()
        {
            CreateMap<College, CollegeListViewModel>();
            CreateMap<College, CollegeDto>();
        }
    }
}