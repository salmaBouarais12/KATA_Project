using KATA.API.DTO.Responses;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KATA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }


        // GET api/<PersonController>/5
        [HttpGet]
        public async Task<IEnumerable<PersonResponse>> Get()
        {
            var persons = await _personService.GetAllPersonsAsync();
            return persons.Select(p=> new PersonResponse(p.Id,p.FirstName,p.LastName));
        }

        // POST api/<PersonController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
