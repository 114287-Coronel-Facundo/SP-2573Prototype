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
    public class ProductsCompanyService : IProductsCompanyService
    {
        private readonly IProductCompanyRepository _productRepository;
        public ProductsCompanyService(IProductCompanyRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            return await _productRepository.Delete(productId);
        }

        public async Task<ProductCompanyViewModel> PostProduct(ProductCompanyViewModel product)
        {
            return await _productRepository.Post(product);
        }

        public async Task<ProductCompanyViewModel> PutProduct(ProductCompanyViewModel product)
        {
            return await _productRepository.Put(product);
        }
    }
}
