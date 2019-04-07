using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Zains.Ostad.Application.Courses.Dtos;
using Zains.Ostad.Application.Courses.Queries.GetCourseDetails;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Teachers.Commands.AddEditCourse
{
    public class AddEditCourseCommandHandler : 
        IRequestHandler<EditCourseCommand, Response>,
        IRequestHandler<AddCourseCommand, Response<CourseDto>>,
        IRequestHandler<UpdateCourseItemByTeacherCommand, Response<CourseItemViewModel>>,
        IRequestHandler<AddCourseItemByTeacherCommand, Response<CourseItemViewModel>>
    {
        private readonly IRepository<Course, long> _courseRepository;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IRepository<CourseItem, long> _courseItemRepository;
        private readonly ICoursesFileManager _coursesFileManager;
        private readonly IUnitOfWork _unitOfWork;

        public AddEditCourseCommandHandler(IRepository<Course, long> courseRepository, IWorkContext workContext,
            ICoursesFileManager coursesFileManager, IMapper mapper, IUnitOfWork unitOfWork, IRepository<CourseItem, long> courseItemRepository, IMediator mediator)
        {
            _courseRepository = courseRepository;
            _workContext = workContext;
            _coursesFileManager = coursesFileManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _courseItemRepository = courseItemRepository;
            _mediator = mediator;
        }

        public async Task<Response<CourseDto>> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            var course = CreateCourseData(request);
            await _courseRepository.AddAsync(course);
            return Response<CourseDto>.Success(await _mediator.Send(new GetCourseDetailsQuery{CourseId = course.Id}, cancellationToken));
        }

        private Course CreateCourseData(AddCourseCommand request)
        {
            return new Course
            {
                Lessons =request.LessonFieldIds.Select(x=>new CourseLessonFieldGradeMapping(){LessonId = x}).ToList(),
                Price = request.Price,
                Description = request.Description,
                CourseTitleId = request.CourseTitleId,
                TeacherId = _workContext.CurrentUserId,
                AdminMessageForTeacher = request.TeacherMessageForAdmin
            };
        }


        public async Task<Response> Handle(EditCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetById(request.CourseId);
            course.Price = request.Price;
            course.ApprovalStatus = CourseApprovalStatus.PendingToApproveByAdmin;
            course.TeacherMessageForAdmin = request.TeacherMessageForAdmin;
            course.Description = request.Description;
            course.CourseTitleId = request.CourseTitleId;
            await _courseRepository.EditAsync(course);
            return Response.Success();
        }

        private ContentType GetContentType(string contentType)
        {
            switch (contentType)
            {
                case "video/mp4":
                case ".mp4": return ContentType.Video;
                case "application/pdf":
                case ".pdf": return ContentType.File;
                default: throw new ArgumentOutOfRangeException(contentType);
            }
        }
        public async Task<Response<CourseItemViewModel>> Handle(AddCourseItemByTeacherCommand request, CancellationToken cancellationToken)
        {
            var course =  _courseRepository.GetQueryable().Include(x=>x.Teacher).First(x=>x.Id==request.CourseId);
            course.HasPendingItemToApprove = true;
            var item = new CourseItem
            {
                Order = request.Order,
                CourseId = request.CourseId,
                TeacherMessageForAdmin = request.TeacherMessageForAdmin,
                State = CourseItemApprovalState.PendingToApproveByAdmin,
                Title = request.Title,
                IsPreview = request.IsPreview,
            };
            
            if (request.File != null)
            {
                await _coursesFileManager.SaveFile(request.File,course.Teacher.UserName, item.CourseId);
                item.FilePath =  _coursesFileManager.GetFilePathForDownload(request.File,course.Teacher.UserName, item.CourseId);
                item.ContentType = GetContentType(request.File.ContentType);
            }

            await _courseItemRepository.AddAsync(item);
            await _courseRepository.EditAsync(course);
            return Response<CourseItemViewModel>.Success(_mapper.Map<CourseItemViewModel>(item));
        }
        public async Task<Response<CourseItemViewModel>> Handle(UpdateCourseItemByTeacherCommand request,
            CancellationToken cancellationToken)
        {
            var item = await _courseItemRepository.GetById(request.Id);
            var course = await _courseRepository.GetById(item.CourseId);
            course.HasPendingItemToApprove = true;
            MapRequestToCourseItemProperties(request, item);
            try
            {
                await HandleUploadedFileAndItemFilePath(request, item);
                await UpdateData(course, item);
            }
            catch (Exception e)
            {
                _unitOfWork.RollBack();
               return Response<CourseItemViewModel>.Failed();
            }
            return Response<CourseItemViewModel>.Success(_mapper.Map<CourseItemViewModel>(item));
        }

        private async Task UpdateData(Course course, CourseItem item)
        {
            _unitOfWork.Begin();
            await _courseRepository.EditAsync(course);
            await _courseItemRepository.EditAsync(item);
            _unitOfWork.Commit();
        }

        private void MapRequestToCourseItemProperties(UpdateCourseItemByTeacherCommand request, CourseItem item)
        {
            item.Order = request.Order;
            item.Title = request.Title;
            item.TeacherMessageForAdmin = request.TeacherMessageForAdmin;
            item.State = CourseItemApprovalState.PendingToApproveByAdmin;
            item.IsPreview = request.IsPreview;
        }

        private async Task HandleUploadedFileAndItemFilePath(UpdateCourseItemByTeacherCommand request, CourseItem item)
        {
            if (request.File != null)
            {
                _coursesFileManager.DeleteFile(item.FilePath);
                var course = _courseRepository.GetQueryable().Include(x => x.Teacher).First(x=>x.Id==request.Id);
                await _coursesFileManager.SaveFile(request.File,course.Teacher.UserName, item.CourseId);
                item.FilePath =  _coursesFileManager.GetFilePathForDownload(request.File,course.Teacher.UserName, item.CourseId);
                item.ContentType = GetContentType(request.File.ContentType);
            }
        }

       
    }
}