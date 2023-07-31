﻿using KATA.API.DTO.Responses;
using KATA.Domain.Interfaces.Sevices;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KATA.API.Controllers;

[Route("api/persons")]
[ApiController]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }


    // GET api/<PersonController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var persons = await _personService.GetAllPersonsAsync();
        var personDetails = persons.Select(p => new PersonResponse(p.Id, p.FirstName, p.LastName));
        var personsResponse = new PersonsResponse(personDetails);
        return Ok (personsResponse);
    }

    // GET api/<PersonController>
    [HttpGet("{id}")]
    public async Task<IEnumerable<PersonResponse>> Get([FromRoute]int id)
    {
        var persons = await _personService.GetPersonByIdAsync(id);
        return persons.Select(p => new PersonResponse(p.Id, p.FirstName, p.LastName));
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
