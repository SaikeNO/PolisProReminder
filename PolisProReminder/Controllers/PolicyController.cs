using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Models.InsurancePolicy;
using PolisProReminder.Services;

namespace PolisProReminder.Controllers
{
    [Route("api/policy")]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicyService _policyService;
        public PolicyController(IPolicyService policyService)
        {
            _policyService = policyService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PolicyDto>> GetAll()
        {
            var policies = _policyService.GetAll();
            return Ok(policies);
        }

        [HttpGet("{id}")]
        public ActionResult<PolicyDto> Get([FromRoute]int id)
        {
            var policy = _policyService.GetById(id);
            return Ok(policy);
        }
    }
}
