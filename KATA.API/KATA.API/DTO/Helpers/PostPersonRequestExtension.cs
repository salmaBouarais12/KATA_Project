using KATA.API.DTO.Requests;
using KATA.Domain.Models;

namespace KATA.API.DTO.Helpers
{
    public static class PostPersonRequestExtension
    {
        public static Person ToPerson(this PostPersonRequest postPersonRequest)
        {
            return new Person { FirstName = postPersonRequest.FirstName, LastName = postPersonRequest.LastName };
        }
    }
}
