using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Models.InsuranceType;
using PolisProReminder.Services;

namespace PolisProReminder.Controllers
{
    [Route("api/type")]
    public class InsuranceTypeController : ControllerBase
    {
        private readonly IInsuranceTypeService _insuranceTypeService;
        public InsuranceTypeController(IInsuranceTypeService insuranceTypeService)
        {
            _insuranceTypeService = insuranceTypeService;
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] CreateInsuranceTypeDto dto, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _insuranceTypeService.Update(id, dto);

            return Ok();

        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _insuranceTypeService.DeleteInsuranceType(id);
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<InsuranceTypeDto> GetById([FromRoute] int id)
        {
            var type = _insuranceTypeService.GetById(id);

            return Ok(type);
        }

        [HttpPost]
        public ActionResult CreateInsuranceType(CreateInsuranceTypeDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = _insuranceTypeService.CreateInsuranceType(dto);

            return Created($"/api/type/{id}", null);
        }


        [HttpGet]
        public ActionResult<InsuranceTypeDto> Get()
        {
            var types = _insuranceTypeService.GetAll();

            return Ok(types);
        }
    }
}
