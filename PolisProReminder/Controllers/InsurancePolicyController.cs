using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Entities;
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
        public ActionResult<IEnumerable<InsurancePolicy>> GetAll()
        {
            var policies = _insurancePolicyService.GetAll();
            return Ok(policies);
        }
    }
}
