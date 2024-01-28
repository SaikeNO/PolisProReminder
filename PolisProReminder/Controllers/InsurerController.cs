using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Models.Insurer;
using PolisProReminder.Services;

namespace PolisProReminder.Controllers
{
    [Route("api/insurer")]
    public class InsurerController : ControllerBase
    {
        private readonly IInsurerService _insurerService;
        public InsurerController(IInsurerService insurerService) 
        {
            _insurerService = insurerService;
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] CreateInsurerDto dto, [FromRoute] int id) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _insurerService.Update(id, dto);

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            _insurerService.DeleteInsurer(id);
            
            return NoContent();
        }

        [HttpPost]
        public ActionResult CreateInsurer([FromBody] CreateInsurerDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = _insurerService.CreateInsurer(dto);

            return Created($"/api/insurer/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<InsurerDto>> GetAll()
        {
            var insurers = _insurerService.GetAll();

            return Ok(insurers);
        }


        [HttpGet("{id}")]
        public ActionResult<InsurerDto> GetById([FromRoute] int id)
        {
            var insurer = _insurerService.GetById(id);

            return Ok(insurer);
        }
    }
}
