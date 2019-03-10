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
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities.Contents;

namespace Zains.Ostad.Application.Teachers.Commands.AddEditCourse
{
    public class AddEditCourseCommandHandler : 
        IRequestHandler<EditCourseCommand, Response>,
        IRequestHandler<AddCourseCommand, Response>,
        IRequestHandler<UpdateCourseItemByTeacherCommand, Response<CourseItemViewModel>>,
        IRequestHandler<AddCourseItemByTeacherCommand, Response<CourseItemViewModel>>
    {
        private readonly IRepository<Course, long> _courseRepository;
        private readonly IRepository<TeacherLessonMapping, long> _teacherLessonMappingRepo;
        private readonly IWorkContext _workContext;
        private readonly IMapper _mapper;
        private readonly IRepository<CourseItem, long> _courseItemRepository;
        private readonly ICoursesFileManager _coursesFileManager;
        private readonly IUnitOfWork _unitOfWork;
        private TeacherLessonMapping _teacherLessonMapping;

        public AddEditCourseCommandHandler(IRepository<Course, long> courseRepository,
            IRepository<TeacherLessonMapping, long> teacherLessonMappingRepo, IWorkContext workContext,
            ICoursesFileManager coursesFileManager, IMapper mapper, IUnitOfWork unitOfWork, IRepository<CourseItem, long> courseItemRepository)
        {
            _courseRepository = courseRepository;
            _teacherLessonMappingRepo = teacherLessonMappingRepo;
            _workContext = workContext;
            _coursesFileManager = coursesFileManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _courseItemRepository = courseItemRepository;
        }

        public async Task<Response> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            var teacherLessonMapping = await GetTeacherLessonMapping(request.LessonFieldId);
            var course = CreateCourseData(request, teacherLessonMapping);
            await _coursesFileManager.SaveFile(request.ZipFile, teacherLessonMapping);
            await _courseRepository.AddAsync(course);
            return Response.Success();
        }

        private Course CreateCourseData(AddCourseCommand request, TeacherLessonMapping teacherLessonMapping)
        {
            return new Course
            {
                Price = request.Price,
                Description = request.Description,
                CourseTitleId = request.CourseTitleId,
                TeacherLessonMappingId = teacherLessonMapping.Id,
                AdminMessageForTeacher = request.TeacherMessageForAdmin,
                ZipFilesPath = _coursesFileManager.GetFilePathForDownload(request.ZipFile, teacherLessonMapping)
            };
        }

        private async Task<TeacherLessonMapping> GetTeacherLessonMapping(long lessonFieldId)
        {
            if (_teacherLessonMapping != null && _teacherLessonMapping.LessonId == lessonFieldId)
                return _teacherLessonMapping;
            _teacherLessonMapping = _teacherLessonMappingRepo.GetQueriable()
                                        .Include(x => x.Teacher)
                                        .Include(x => x.LessonFieldMapping)
                                        .Include(x => x.LessonFieldMapping.Field)
                                        .Include(x => x.LessonFieldMapping.Lesson)
                                        .Include(x => x.LessonFieldMapping.Grade)
                                        .FirstOrDefault(x =>
                                            x.TeacherId == _workContext.CurrentUserId &&
                                            x.LessonId == lessonFieldId) ??
                                    await CreateNewTeacherLessonMapping(lessonFieldId,
                                        _workContext.CurrentUserId);
            return _teacherLessonMapping;
        }

        private async Task<TeacherLessonMapping> CreateNewTeacherLessonMapping(long lessonFieldId,
            long teacherId)
        {
            var mapping = new TeacherLessonMapping
            {
                TeacherId = teacherId,
                LessonId = lessonFieldId
            };
            await _teacherLessonMappingRepo.AddAsync(mapping);
            return _teacherLessonMappingRepo.GetQueriable()
                .Include(x => x.Teacher)
                .Include(x => x.LessonFieldMapping)
                .Include(x => x.LessonFieldMapping.Field)
                .Include(x => x.LessonFieldMapping.Lesson)
                .Include(x => x.LessonFieldMapping.Grade)
                .First(x => x.Id == mapping.Id);
        }

        public async Task<Response> Handle(EditCourseCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetById(request.CourseId);
            var teacherLessonMapping = await GetTeacherLessonMapping(request.LessonFieldId);
            course.Price = request.Price;
            course.ApprovalStatus = CourseApprovalStatus.PendingToApproveByAdmin;
            course.TeacherMessageForAdmin = request.TeacherMessageForAdmin;
            course.Description = request.Description;
            course.CourseTitleId = request.CourseTitleId;
            course.TeacherLessonMappingId = teacherLessonMapping.Id;
            await _courseRepository.EditAsync(course);
            return Response.Success();
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
        public async Task<Response<CourseItemViewModel>> Handle(AddCourseItemByTeacherCommand request, CancellationToken cancellationToken)
        {
            var course = await _courseRepository.GetById(request.CourseId);
            course.PendingToApproveItemsCount  += 1;
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
                await _coursesFileManager.SaveFile(request.File, item.CourseId);
                item.FilePath = await _coursesFileManager.GetFilePathForDownload(request.File, item.CourseId);
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
            course.PendingToApproveItemsCount += 1;
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
                await _coursesFileManager.SaveFile(request.File, item.CourseId);
                item.FilePath = await _coursesFileManager.GetFilePathForDownload(request.File, item.CourseId);
                item.ContentType = GetContentType(request.File.ContentType);
            }
        }

       
    }
}