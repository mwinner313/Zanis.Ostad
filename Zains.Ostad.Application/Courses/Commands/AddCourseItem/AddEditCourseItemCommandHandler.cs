using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Zains.Ostad.Application.Courses.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Courses.Commands.AddCourseItem
{
    public class AddCourseItemCommandHandler : IRequestHandler<AddCourseItemCommand, CourseItemViewModel>,
        IRequestHandler<EditCourseItemCommand, CourseItemViewModel>
    {
        private readonly ICoursesFileManager _coursesFileManager;
        private readonly IRepository<Course, long> _courseRepo;
        private readonly IRepository<CourseItem, long> _courseItemRepo;
        private readonly IMapper _mapper;

        public AddCourseItemCommandHandler(ICoursesFileManager coursesFileManager,
            IRepository<CourseItem, long> courseItemRepo, IMapper mapper, IRepository<Course, long> courseRepo)
        {
            _coursesFileManager = coursesFileManager;
            _courseItemRepo = courseItemRepo;
            _mapper = mapper;
            _courseRepo = courseRepo;
        }

        public async Task<CourseItemViewModel> Handle(AddCourseItemCommand request, CancellationToken cancellationToken)
        {
            var course =  _courseRepo.GetQueryable().Include(x=>x.Teacher).First(x=>x.Id==request.CourseId);
            var item = new CourseItem
            {
                CourseId = request.CourseId,
                State = request.State,
                AdminMessageForTeacher = request.AdminMessageForTeacher,
                Order = request.Order !=0?request.Order:_courseItemRepo.GetQueryable().Count(x=>x.CourseId==request.CourseId) + 1 ,
                Title = request.Title,
                ContentType = request.File !=null? GetContentType(request.File.ContentType):ContentType.File,
                FilePath =  _coursesFileManager.GetFilePathForDownload(request.File,course.Teacher.UserName, request.CourseId),
                IsPreview = request.IsPreview
            };
            await _coursesFileManager.SaveFile(request.File,course.Teacher.UserName, request.CourseId);
            await _courseItemRepo.AddAsync(item);
            return _mapper.Map<CourseItemViewModel>(item);
        }

        public async Task<CourseItemViewModel> Handle(EditCourseItemCommand request,
            CancellationToken cancellationToken)
        {
            var item = await _courseItemRepo.GetById(request.Id);
            await HandleCoursePendingToApproveItemCount(request, item);
            MapRequestToProperties(request, item);
            await HandleUploadedFileAndItemFilePath(request, item);
            await _courseItemRepo.EditAsync(item);
            return _mapper.Map<CourseItemViewModel>(item);
        }

        private void MapRequestToProperties(EditCourseItemCommand request, CourseItem item)
        {
            item.State = request.State;
            item.Order = request.Order;
            item.Title = request.Title;
            item.AdminMessageForTeacher = request.AdminMessageForTeacher;
            item.IsPreview = request.IsPreview;
            item.ContentType = request.File != null ? GetContentType(request.File.ContentType) : ContentType.File;
        }

        private async Task HandleUploadedFileAndItemFilePath(EditCourseItemCommand request, CourseItem item)
        {
            if (request.File != null)
            {
                if(File.Exists(item.FilePath))
                _coursesFileManager.DeleteFile(item.FilePath);
                var course = _courseRepo.GetQueryable().Include(x => x.Teacher)
                    .First(x => x.Contents.Any(c => c.Id == request.Id));
                await _coursesFileManager.SaveFile(request.File,course.Teacher.UserName, item.CourseId);
                item.FilePath =  _coursesFileManager.GetFilePathForDownload(request.File,course.Teacher.UserName, item.CourseId);
            }
        }

        private async Task HandleCoursePendingToApproveItemCount(EditCourseItemCommand request, CourseItem item)
        {
            if (item.State == CourseItemApprovalState.PendingToApproveByAdmin &&
                request.State != CourseItemApprovalState.PendingToApproveByAdmin)
            {
                var course = await _courseRepo.GetById(item.CourseId);
                course.HasPendingItemToApprove = true ;
                await _courseRepo.EditAsync(course);
            }
        }

        private ContentType GetContentType(string contentType)
        {
            if (contentType.StartsWith("video"))
                return ContentType.Video;
            if (contentType == "application/pdf")
                return ContentType.File;
            throw new ArgumentOutOfRangeException(contentType);
        }
    }
}