using System.Threading.Tasks;
using AGLCodingTest.Application.Interfaces;
using AGLCodingTest.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace AGLCodingTest.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonService personService;

        public PersonController(IPersonService service)
        {
            this.personService = service;
        }

        // GET : api/person/pet/cat
        [HttpGet("pet/{petType}")]
        public async Task<IActionResult> Get(AnimalType petType)
        {
            return Ok(await personService.GetPetsByOwnerGenderAsync(petType));
        }
    }
}