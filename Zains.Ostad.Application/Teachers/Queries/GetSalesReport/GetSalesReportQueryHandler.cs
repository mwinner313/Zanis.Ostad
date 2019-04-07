using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zains.Ostad.Application.Infrastucture;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.Cart;
using Zanis.Ostad.Core.Entities.Contents;
using Zanis.Ostad.Core.Entities.Orders;

namespace Zains.Ostad.Application.Teachers.Queries.GetSalesReport
{
    public class GetSalesReportQueryHandler : IRequestHandler<GetSalesReportQuery, PagenatedList<CourseSaleReport>>
    {
        private readonly IRepository<OrderItem, long> _orderItemRepo;
        private readonly IRepository<Course, long> _courseRepo;
        private readonly IWorkContext _workContext;

        public GetSalesReportQueryHandler(IRepository<OrderItem, long> orderItemRepo,
            IRepository<Course, long> courseRepo, IWorkContext workContext)
        {
            _orderItemRepo = orderItemRepo;
            _courseRepo = courseRepo;
            _workContext = workContext;
        }

        public async Task<PagenatedList<CourseSaleReport>> Handle(GetSalesReportQuery request,
            CancellationToken cancellationToken)
        {
            var teacherCourseIds = _courseRepo.GetQueryable()
                .Where(x => x.TeacherId == _workContext.CurrentUserId).Select(x => x.Id).ToList();
            var saleReports = _orderItemRepo.GetQueryable().Where(x =>
                    x.ProductType == ProductType.TeacherCourse && teacherCourseIds.Contains(x.ProductId))
                .Select(x => new CourseSaleReport
                {
                    UserFullName = x.Order.User.FullName
                });
            return new PagenatedList<CourseSaleReport>
            {
                Items = saleReports.Pagenate(request).ToList(),
                AllCount = saleReports.Count()
            };
        }
    }
}