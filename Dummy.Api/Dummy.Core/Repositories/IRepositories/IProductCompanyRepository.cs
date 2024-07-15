using Dummy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Core.Repositories.IRepositories
{
    public interface IProductCompanyRepository
    {
        Task<ProductCompanyViewModel> Post(ProductCompanyViewModel model);
        Task<ProductCompanyViewModel> Put(ProductCompanyViewModel model);
        Task<bool> Delete(int id);   
    }
}
