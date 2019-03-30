using MediatR;
using Zains.Ostad.Application.Infrastucture;

namespace Zains.Ostad.Application.Teachers.Queries.GetSalesReport
{
    public class GetSalesReportQuery:Pagenation,IRequest<PagenatedList<CourseSaleReport>>
    {
        
    }
}