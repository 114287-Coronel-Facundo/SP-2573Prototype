using Dummy.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dummy.Core.Services.IServices
{
    public interface IProductService
    {
        Task<ProductViewModel> PostProduct(ProductViewModel productViewModel);
        Task<ProductViewModel> PutProduct(ProductViewModel productViewModel);
    }
}
