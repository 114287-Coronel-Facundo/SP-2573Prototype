using Dummy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Core.Repositories.IRepositories
{
    public
        interface ICompanyRepository
    {
        Task<CompanyViewModel> PostCompany(CompanyViewModel company);
        Task<CompanyViewModel> PutCompany(CompanyViewModel company);
        Task<bool> DeleteCompany(int companyId);
    }
}
