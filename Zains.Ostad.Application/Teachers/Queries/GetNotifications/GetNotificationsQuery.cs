using MediatR;
using Zains.Ostad.Application.Infrastucture;

namespace Zains.Ostad.Application.Teachers.Queries.GetNotifications
{
    public class GetNotificationsQuery:Pagenation,IRequest<PagenatedList<NotificationViewModel>>
    {
        public bool JustNewOnes { get; set; }
        public bool NoPaginate { get; set; }
    }
}