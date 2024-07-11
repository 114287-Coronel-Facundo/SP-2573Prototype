using AutoMapper;
using Dummy.Core.Model.Classes;
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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;


        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<ProductViewModel> PostProduct(ProductViewModel productViewModel)
        {
            return _productRepository.PostProduct(productViewModel);
        }

        public async Task<ProductViewModel> PutProduct(ProductViewModel productViewModel)
        {
            return (await _productRepository.PutProduct(productViewModel));
        }
    }
}
