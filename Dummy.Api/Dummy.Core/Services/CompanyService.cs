using Dummy.Core.Repositories.IRepositories;
using Dummy.Core.Services.IServices;
using Dummy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Core.Services
{
    public class CompanyService : ICompanyService
    { 
        private readonly ICompanyRepository companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public CompanyService() { }
        public async Task<bool> DeleteCompany(int companyId)
        {
            return await companyRepository.DeleteCompany(companyId);
            
        }

        public async Task<CompanyViewModel> PostCompany(CompanyViewModel company)
        {
            return await companyRepository.PostCompany(company);
        }

        public async Task<CompanyViewModel> PutCompany(CompanyViewModel company)
        {
            return await companyRepository.PutCompany(company);
        }
    }
}
