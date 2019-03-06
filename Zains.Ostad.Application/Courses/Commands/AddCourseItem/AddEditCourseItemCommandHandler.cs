using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
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
            var item = new CourseItem
            {
                CourseId = request.CourseId,
                State = request.State,
                Order = request.Order,
                Title = request.Title,
                ContentType = GetContentType(request.File.ContentType),
                FilePath = await _coursesFileManager.GetFilePath(request.File, request.CourseId),
                IsPreview = request.IsPreview
            };
            await _coursesFileManager.SaveFile(request.File, request.CourseId);
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
            item.IsPreview = request.IsPreview;
            item.ContentType = GetContentType(request.File.ContentType);
        }

        private async Task HandleUploadedFileAndItemFilePath(EditCourseItemCommand request, CourseItem item)
        {
            if (request.File != null)
            {
                _coursesFileManager.DeleteFile(item.FilePath);
                await _coursesFileManager.SaveFile(request.File, item.CourseId);
                item.FilePath = await _coursesFileManager.GetFilePath(request.File, item.CourseId);
            }
        }

        private async Task HandleCoursePendingToApproveItemCount(EditCourseItemCommand request, CourseItem item)
        {
            if (item.State == CourseItemApprovalState.PendingToApproveByAdmin &&
                request.State != CourseItemApprovalState.PendingToApproveByAdmin)
            {
                var course = await _courseRepo.GetById(item.CourseId);
                course.PendingToApproveItemsCount -= 1;
                await _courseRepo.EditAsync(course);
            }
        }

        private ContentType GetContentType(string contentType)
        {
            switch (contentType)
            {
                case ".mp4": return ContentType.Video;
                case ".pdf": return ContentType.File;
                default: throw new ArgumentOutOfRangeException(contentType);
            }
        }
    }
}