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

        [HttpGet]
        public ActionResult<IEnumerable<InsurerDto>> GetAll()
        {
            var insurers  = _insurerService.GetAll();

            return _mapper.Map<List<InsurerDto>>(insurers);
        }

    }
}
