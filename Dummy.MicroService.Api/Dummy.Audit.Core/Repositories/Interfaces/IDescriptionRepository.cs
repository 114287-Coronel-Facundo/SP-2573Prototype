using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Audit.Core.Repositories.Interfaces
{
    public interface IDescriptionRepository
    {
        Task<(string, string)> GetDescription(string tableName, string descriptionColumn, string id);
    }
}
