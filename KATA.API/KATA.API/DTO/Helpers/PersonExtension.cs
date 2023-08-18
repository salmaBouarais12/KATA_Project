using KATA.API.DTO.Responses;
using KATA.Domain.Models;

namespace KATA.API.DTO.Helpers
{
    public static class PersonExtension
    {
        public static PersonResponse ToPersonResponse (this Person person)
        {
            return new PersonResponse(person.Id, person.FirstName, person.LastName);
        }
    }
}
