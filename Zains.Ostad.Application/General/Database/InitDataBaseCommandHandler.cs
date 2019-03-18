using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zains.Ostad.Application.Users;
using Zanis.Ostad.Core.Contracts;
using Zanis.Ostad.Core.Dtos;
using Zanis.Ostad.Core.Entities;
using Zanis.Ostad.Core.Entities.Tickets;

namespace Zains.Ostad.Application.General.Database
{
    public class InitDataBaseCommandHandler : IRequestHandler<InitDataBaseCommand, Response>
    {
        private readonly IRepository<TicketCategory, int> _ticketCategoryRepo;
        private readonly IAppRoleManager _roleManager;

        public InitDataBaseCommandHandler(IRepository<TicketCategory, int> ticketCategoryRepo,
            IAppRoleManager roleManager)
        {
            _ticketCategoryRepo = ticketCategoryRepo;
            _roleManager = roleManager;
        }

        public async Task<Response> Handle(InitDataBaseCommand request, CancellationToken cancellationToken)
        {
            await CreateDefaultRoles();
            await CreateDefaultTicketCategories();
            return Response.Success();
        }

    

        private async Task CreateDefaultRoles()
        {
            var list = new List<string>
            {
                "Admin",
                "Operator",
                "Teacher",
                "Editor"
            };
            foreach (var role in list)
            {
                if (_roleManager.Roles.All(x => x.Name != role))
                    await _roleManager.CreateAsync(new Role
                    {
                        Name = role
                    });
            }
        }

        private async Task CreateDefaultTicketCategories()
        {
            if (_ticketCategoryRepo.GetQueryable().All(x => x.CategoryType != TicketCategoryType.RelatedToTeacher))
                await _ticketCategoryRepo.AddAsync(new TicketCategory
                {
                    Title = "دوره اموزشی استاد",
                    CategoryType = TicketCategoryType.RelatedToTeacher,
                    IsDeletible = false
                });
            if (_ticketCategoryRepo.GetQueryable().All(x => x.CategoryType != TicketCategoryType.RelatedToSupport))
                await _ticketCategoryRepo.AddAsync(new TicketCategory
                {
                    Title = "پشتیبانی",
                    CategoryType = TicketCategoryType.RelatedToSupport,
                    IsDeletible = false
                });
            if (_ticketCategoryRepo.GetQueryable().All(x => x.CategoryType != TicketCategoryType.RelatedToRequest))
            {
                await _ticketCategoryRepo.AddAsync(new TicketCategory
                {
                    Title = "درخواست دوره",
                    CategoryType = TicketCategoryType.RelatedToRequest,
                    IsDeletible = false
                });
                await _ticketCategoryRepo.AddAsync(new TicketCategory
                {
                    Title = "درخواست کلاس خصوصی",
                    CategoryType = TicketCategoryType.RelatedToRequest,
                    IsDeletible = false
                });
            }
             
        }
    }
}