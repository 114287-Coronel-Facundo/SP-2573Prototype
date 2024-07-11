using Dummy.Core.Services.IServices;
using Dummy.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dummy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductViewModel product)
        {
            return Ok(_productService.PostProduct(product));
        }

        [HttpPut]
        public async Task<IActionResult> Put(ProductViewModel product)
        {
            return Ok(_productService.PutProduct(product).Result);
        }

    }
}
