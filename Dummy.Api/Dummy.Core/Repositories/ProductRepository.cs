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
    public class ProductRepository : IProductRepository
    {
        private readonly DomainContext domainContext;
        private readonly IMapper _mapper;

        public ProductRepository(DomainContext domainContext, IMapper mapper)
        {
            this.domainContext = domainContext;
            _mapper = mapper;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            domainContext.Products.Remove(domainContext.Products.FirstOrDefault(p => p.Id == productId));
            domainContext.SaveChanges();
            return true;
        }

        public async Task<ProductViewModel> PostProduct(ProductViewModel product)
        {
            var produtToPost = _mapper.Map<Product>(product);
            domainContext.Add(produtToPost);
            domainContext.SaveChanges();

            return product;
        }

        public async Task<ProductViewModel> PutProduct(ProductViewModel product)
        {
            var productToPut = domainContext.Products.FirstOrDefault(p => p.Id == product.Id);

            if(product != null)
            {
                productToPut.Price = product.Price;
                productToPut.Description = product.Description;
                domainContext.Update(productToPut);
                await domainContext.SaveChangesAsync();
                return product;
            }
            else
            {
                throw new Exception("Error put");
            }
        }
    }
}
