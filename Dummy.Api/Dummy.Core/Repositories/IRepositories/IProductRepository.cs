using Dummy.Core.Model.Classes;
using Dummy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Core.Repositories.IRepositories
{
    public interface IProductRepository
    {
        Task<ProductViewModel> PostProduct(ProductViewModel product);
        Task<ProductViewModel> PutProduct(ProductViewModel product);
        Task<bool> DeleteProduct(int productId);
    }
}
