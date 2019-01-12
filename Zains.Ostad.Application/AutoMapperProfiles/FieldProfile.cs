using AutoMapper;
using Zains.Ostad.Application.Colleges.Queries.GetCollegeList;
using Zains.Ostad.Application.Fields.Queries;
using Zains.Ostad.Application.Fields.Queries.GetField;
using Zains.Ostad.Application.Fields.Queries.GetFieldsList;
using Zanis.Ostad.Core.Entities;

namespace Zains.Ostad.Application.AutoMapperProfiles
{
    public class FieldProfile:Profile
    {
        public FieldProfile()
        {
            CreateMap<Field, FieldListViewModel>();
            CreateMap<Field, FieldLDto>();
        }
    }
}