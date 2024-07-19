using Dummy.Core.Services.IServices;
using Dummy.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dummy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCompanyController : ControllerBase
    {
        private readonly IProductsCompanyService _productsCompanyService;

        public ProductCompanyController(IProductsCompanyService productsCompanyService)
        {
            _productsCompanyService = productsCompanyService;
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productsCompanyService.DeleteProduct(productId);
            if (result)
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
        }

        [HttpPost]
        public async Task<IActionResult> PostProduct(ProductCompanyViewModel product)
        {
            var result = await _productsCompanyService.PostProduct(product);
            if (result != null)
            {
                return Ok(result);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, "Error adding data");
        }

    }
}
