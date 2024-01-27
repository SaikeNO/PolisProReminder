using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Models;
using PolisProReminder.Services;

namespace PolisProReminder.Controllers
{
    [Route("api/insurancePolicy")]
    public class InsurancePolicyController : ControllerBase
    {
        private readonly IInsurancePolicySerivce _insurancePolicyService;
        public InsurancePolicyController(IInsurancePolicySerivce insurancePolicyService)
        {
            _insurancePolicyService = insurancePolicyService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InsurancePolicyDto>> GetAll()
        {
            var policies = _insurancePolicyService.GetAll();
            return Ok(policies);
        }

        [HttpGet("{id}")]
        public ActionResult<InsurancePolicyDto> Get([FromRoute]int id)
        {
            var policy = _insurancePolicyService.GetById(id);
            return Ok(policy);
        }
    }
}
