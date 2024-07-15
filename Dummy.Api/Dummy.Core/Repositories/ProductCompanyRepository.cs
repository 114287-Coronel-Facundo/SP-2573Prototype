using AutoMapper;
using Dummy.Core.Model;
using Dummy.Core.Repositories.IRepositories;
using Dummy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Core.Repositories
{
    public class ProductCompanyRepository : IProductCompanyRepository
    {
        private readonly IMapper mapper;
        private readonly DomainContext domainContext;

        public ProductCompanyRepository(IMapper mapper, DomainContext domainContext)
        {
            this.mapper = mapper;
            this.domainContext = domainContext;
        }

        public async Task<bool> Delete(int id)
        {
            domainContext.ProductsCompanies.Remove(domainContext.ProductsCompanies.FirstOrDefault(pc => pc.Id == id));
            return true;
        }

        public Task<ProductCompanyViewModel> Post(ProductCompanyViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task<ProductCompanyViewModel> Put(ProductCompanyViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
