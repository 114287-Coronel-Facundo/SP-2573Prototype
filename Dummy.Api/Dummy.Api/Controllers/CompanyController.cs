using Dummy.Core.Services.IServices;
using Dummy.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dummy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CompanyViewModel company)
        {
            return Ok(_companyService.PostCompany(company).Result);
        }

        [HttpPut]
        public async Task<IActionResult> Put(CompanyViewModel company)
        {
            return Ok(_companyService.PutCompany(company).Result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int companyId)
        {
            return Ok(_companyService.DeleteCompany(companyId).Result);
        }

    }
}
