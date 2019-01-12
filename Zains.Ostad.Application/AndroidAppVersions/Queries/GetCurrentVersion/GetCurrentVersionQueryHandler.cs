using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Zains.Ostad.Application.AndroidAppVersions.Dtos;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Entities.AndroidApp;

namespace Zains.Ostad.Application.AndroidAppVersions.Queries.GetCurrentVersion
{
    public class GetCurrentVersionQueryHandler:IRequestHandler<GetCurrentVersionQuery,AppVersionViewModel>
    {
        private readonly IRepository<AppVersion, int> _repository;
        private readonly IMapper _mapper;
        public GetCurrentVersionQueryHandler(IRepository<AppVersion, int> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AppVersionViewModel> Handle(GetCurrentVersionQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<AppVersionViewModel>(_repository.GetQueriable().Include(x=>x.Features).OrderByDescending(x => x.Id)
                .FirstOrDefault());
        }
    }
}