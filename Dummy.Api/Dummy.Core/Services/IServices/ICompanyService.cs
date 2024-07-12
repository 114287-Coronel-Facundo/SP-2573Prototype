using Dummy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Core.Services.IServices
{
    public interface ICompanyService
    {
        Task<CompanyViewModel> PostCompany(CompanyViewModel company);
        Task<CompanyViewModel> PutCompany(CompanyViewModel company);
        Task<bool> DeleteCompany(int companyId);
    }
}
