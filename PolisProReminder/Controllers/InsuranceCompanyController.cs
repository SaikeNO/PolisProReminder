using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Models;
using PolisProReminder.Services;

namespace PolisProReminder.Controllers
{
    [Route("api/company")]
    public class InsuranceCompanyController : ControllerBase
    {
        private readonly IInsuranceCompanyService _insuranceCompanyService;

        public InsuranceCompanyController(IInsuranceCompanyService insuranceCompanyService)
        {
            _insuranceCompanyService = insuranceCompanyService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InsuranceCompanyDto>> GetAll()
        {
            var companies = _insuranceCompanyService.GetAll();

            return Ok(companies);
        }

        [HttpGet("{id}")]
        public ActionResult<InsuranceCompanyDto> Get([FromRoute] int id)
        {
            var company = _insuranceCompanyService.Get(id);
            return Ok(company);
        }
    }
}
