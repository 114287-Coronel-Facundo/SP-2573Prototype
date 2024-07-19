using Dummy.Core.ViewModels;

namespace Dummy.Core.Services.IServices
{
    public interface IProductsCompanyService
    {
        Task<ProductCompanyViewModel> PostProduct(ProductCompanyViewModel product);
        Task<ProductCompanyViewModel> PutProduct(ProductCompanyViewModel product);
        Task<bool> DeleteProduct(int productId);
    }
}
