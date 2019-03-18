using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Zains.Ostad.Application.Infrastucture;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Notifications;

namespace Zains.Ostad.Application.Teachers.Queries.GetNotifications
{
    public class
        GetNotificationsQueryHandler : IRequestHandler<GetNotificationsQuery, PagenatedList<NotificationViewModel>>
    {
        private readonly IWorkContext _workContext;
        private readonly IRepository<Notification, long> _notifRepo;
        private readonly IMapper _mapper;

        public GetNotificationsQueryHandler(IWorkContext workContext, IRepository<Notification, long> notifRepo,
            IMapper mapper)
        {
            _workContext = workContext;
            _notifRepo = notifRepo;
            _mapper = mapper;
        }

        public async Task<PagenatedList<NotificationViewModel>> Handle(GetNotificationsQuery request,
            CancellationToken cancellationToken)
        {
            var queryable = _notifRepo.GetQueryable();
            if (request.JustNewOnes)
                queryable = queryable.Where(x => !x.IsSeen);
            queryable = queryable.Where(x => x.ReceiverId == _workContext.CurrentUserId)
                .OrderByDescending(x => x.CreatedOn);

            return new PagenatedList<NotificationViewModel>
            {
                AllCount = queryable.Count(),
                Items = _mapper.Map<List<NotificationViewModel>>(
                    request.NoPaginate
                        ? queryable.ToList()
                        : queryable.Pagenate(request).ToList()
                )
            };
        }
    }
}