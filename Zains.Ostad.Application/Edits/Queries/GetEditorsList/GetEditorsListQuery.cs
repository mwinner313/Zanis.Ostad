using MediatR;
using Zains.Ostad.Application.Infrastucture;
using Zains.Ostad.Application.Users.Dto;

namespace Zains.Ostad.Application.Edits.Queries.GetEditorsList
{
    public class GetEditorsListQuery :Pagenation, IRequest<PagenatedList<UserDto>>
    {
        public string Search { get; set; }
    }
}