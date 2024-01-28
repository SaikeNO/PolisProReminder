using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PolisProReminder.Models.Insurer;
using PolisProReminder.Services;

namespace PolisProReminder.Controllers
{
    [Route("api/insurer")]
    public class InsurerController : ControllerBase
    {
        private readonly IInsurerService _insurerService;
        private readonly IMapper _mapper;
        public InsurerController(IInsurerService insurerService, IMapper mapper) 
        {
            _insurerService = insurerService;
            _mapper = mapper;
        }

        [HttpPut("{id}")]
        public ActionResult Udpate([FromBody] CreateInsurerDto dto, [FromRoute] int id) 
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
            var insurers  = _insurerService.GetAll();

            return _mapper.Map<List<InsurerDto>>(insurers);
        }


        [HttpGet("{id}")]
        public ActionResult<InsurerDto> GetById([FromRoute] int id)
        {
            var insurer = _insurerService.GetById(id);

            return _mapper.Map<InsurerDto>(insurer);
        }
    }
}
