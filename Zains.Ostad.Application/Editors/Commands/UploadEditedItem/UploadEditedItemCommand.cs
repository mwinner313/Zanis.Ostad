using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Zanis.Ostad.Core.Dtos;

namespace Zains.Ostad.Application.Editors.Commands.UploadEditedItem
{
    public class UploadEditedItemCommand:IRequest<Response>
    {
        public long EditAssignmnetId { get; set; }
        public IFormFile File { get; set; }
    }

    public class UploadEditedItemCommandHandler:IRequestHandler<UploadEditedItemCommand,Response>
    {
        public Task<Response> Handle(UploadEditedItemCommand request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}