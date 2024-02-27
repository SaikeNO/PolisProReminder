using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Models;
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

        [HttpPost]
        public ActionResult CreatePolicy([FromBody] CreatePolicyDto dto)
        {
            if(!ModelState.IsValid) 
                return BadRequest(ModelState);  

            var id = _policyService.CreatePolicy(dto);

            return Created($"api/policy/{id}", null);
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
