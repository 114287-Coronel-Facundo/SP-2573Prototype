using Dummy.Audit.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<UserViewModel>> GetUsers(List<int> userIds);
    }
}
