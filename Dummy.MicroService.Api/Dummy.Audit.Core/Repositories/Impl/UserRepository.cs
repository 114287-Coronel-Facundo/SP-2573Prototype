using Dummy.Audit.Core.Model;
using Dummy.Audit.Core.Repositories.Interfaces;
using Dummy.Audit.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly DomainContext _domainContext;

        public UserRepository(DomainContext domainContext)
        {
            _domainContext = domainContext;
        }


        public async Task<List<UserViewModel>> GetUsers(List<int> userIds)
        {
            var query = _domainContext.Users.AsQueryable().Include(p => p.Person);

            return  query.Where(p => userIds.Contains(p.Id)).Select(p => new UserViewModel
            {
                Id = p.Id,
                Name = p.Person.Name,
                Surname = p.Person.Surname,
                LegalStatusId = p.Person.LegalStatusId,
                BusinessName = p.Person.BusinessName,
            }).ToList();
        }
    }
}
