using AutoMapper;
using Dummy.Core.Model;
using Dummy.Core.Model.Classes;
using Dummy.Core.Repositories.IRepositories;
using Dummy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Core.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IMapper mapper;
        private readonly DomainContext domainContext;

        public CompanyRepository(IMapper mapper, DomainContext domainContext)
        {
            this.mapper = mapper;
            this.domainContext = domainContext;
        }

        public async Task<bool> DeleteCompany(int companyId)
        {
            domainContext.Companies.Remove(domainContext.Companies.FirstOrDefault(c => c.Id == companyId));
            await domainContext.SaveChangesAsync();
            return true;
        }

        public async Task<CompanyViewModel> PostCompany(CompanyViewModel company)
        {
            await domainContext.Companies.AddAsync(mapper.Map<Company>(company));
            await domainContext.SaveChangesAsync();
            return company;
        }

        public async Task<CompanyViewModel> PutCompany(CompanyViewModel company)
        {
            var toUpdate = domainContext.Companies.FirstOrDefault(c => c.Id == company.Id);
            if (toUpdate == null)
            {
                throw new Exception("Company not found");
            }
            toUpdate.Id = company.Id;
            toUpdate.Name = company.Name;
            domainContext.Companies.Update(toUpdate);
            domainContext.SaveChanges();
            return company;
        }
    }
}
