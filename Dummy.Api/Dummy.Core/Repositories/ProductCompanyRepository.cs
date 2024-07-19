using AutoMapper;
using Dummy.Core.Model;
using Dummy.Core.Model.Classes;
using Dummy.Core.Repositories.IRepositories;
using Dummy.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
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
            await domainContext.SaveChangesAsync();
            return true;
        }

        public async Task<ProductCompanyViewModel> Post(ProductCompanyViewModel model)
        {
            //var toPost = mapper.Map<ProductsCompany>(model);
            //domainContext.Add(toPost);
            //await domainContext.SaveChangesAsync();
            //return model;
            using (var context = domainContext.Database.BeginTransaction())
            {
                try
                {
                    var toPost = mapper.Map<ProductsCompany>(model);
                    domainContext.Add(toPost);
                    await domainContext.SaveChangesAsync();
                    //context.Commit();
                    context.Commit();
                    return model;
                }
                catch (Exception ex)
                {
                    context.Rollback();
                    throw ex;
                }

            }
        }

        public async Task<ProductCompanyViewModel> Put(ProductCompanyViewModel model)
        {
            var entity = domainContext.ProductsCompanies.FirstOrDefault(x => x.Id == model.Id);
            entity.ProductId = model.ProductId;
            entity.CompanyId = model.CompanyId;

            domainContext.Update(entity);
            await domainContext.SaveChangesAsync();
            return model;
        }
    }
}
