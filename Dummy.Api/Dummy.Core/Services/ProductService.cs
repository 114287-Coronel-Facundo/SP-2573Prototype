using Dummy.Core.Repositories.IRepositories;
using Dummy.Core.Services.IServices;
using Dummy.Core.ViewModels;

namespace Dummy.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;


        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            return await _productRepository.DeleteProduct(productId);
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
