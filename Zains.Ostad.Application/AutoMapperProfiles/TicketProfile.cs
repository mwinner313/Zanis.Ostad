using System;
using AutoMapper;
using Zains.Ostad.Application.Tickets.Dtos;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.AutoMapperProfiles
{
    public class TicketProfile : Profile
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketViewModel>().ForMember(x => x.CourseTitle,
                opt => opt.MapFrom(x =>
                    x.CourseId.HasValue
                        ? x.Course.CourseTitle.Name + " - " +
                          x.Course.TeacherLessonMapping.LessonFieldMapping.Lesson.LessonName
                        : ""));
            CreateMap<Ticket, TicketListViewModel>().ForMember(x => x.CourseTitle,
                opt => opt.MapFrom(x =>
                    x.CourseId.HasValue
                        ? x.Course.CourseTitle.Name + " - " +
                          x.Course.TeacherLessonMapping.LessonFieldMapping.Lesson.LessonName
                        : ""));
            CreateMap<TicketItem, TicketItemViewModel>();
        }
    }
}