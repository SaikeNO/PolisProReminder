using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Models.InsurancePolicy;
using PolisProReminder.Services;

namespace PolisProReminder.Controllers
{
    [Route("api/policy")]
    public class PolicyController : ControllerBase
    {
        private readonly IInsurancePolicySerivce _insurancePolicyService;
        public PolicyController(IInsurancePolicySerivce insurancePolicyService)
        {
            _insurancePolicyService = insurancePolicyService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PolicyDto>> GetAll()
        {
            var policies = _insurancePolicyService.GetAll();
            return Ok(policies);
        }

        [HttpGet("{id}")]
        public ActionResult<PolicyDto> Get([FromRoute]int id)
        {
            var policy = _insurancePolicyService.GetById(id);
            return Ok(policy);
        }
    }
}
