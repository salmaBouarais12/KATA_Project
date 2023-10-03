﻿using KATA.API.DTO.Helpers;
using KATA.API.DTO.Requests;
using KATA.API.DTO.Responses;
using KATA.Domain.Interfaces.Sevices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KATA.API.Controllers;

[Authorize]
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
        var personDetails = persons.Select(p => p.ToPersonResponse());
        // var personDetails = persons.Select(PersonExtension.ToPersonResponse);
        var personsResponse = new PersonsResponse(personDetails);
        return Ok(personsResponse);
    }

    // GET api/<PersonController>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var person = await _personService.GetPersonByIdAsync(id);
        if (person is not null)
        {
            return Ok(person.ToPersonResponse());
        }
        return NotFound();
    }

    // POST api/<PersonController>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PostPersonRequest personRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var personAdded = await _personService.AddPersonsAsync(personRequest.ToPerson());
        return Ok(personAdded);
    }

    // PUT api/<PersonController>/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] PostPersonRequest personRequest)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest();
        }
        var updatePerson = await _personService.UpdatePersonsAsync(id, personRequest.ToPerson());
        if (updatePerson == null)
        {
            return NotFound();
        }
        else
        {
            return Ok(new PersonResponse(updatePerson.Id, updatePerson.FirstName, updatePerson.LastName));
        }
    }

    // DELETE api/<PersonController>/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var personToBeDeleted = await _personService.DeletePersonsAsync(id);
        if (personToBeDeleted == null) return NotFound();
        return Ok(new PersonResponse(personToBeDeleted.Id, personToBeDeleted.FirstName, personToBeDeleted.LastName));
    }
}
