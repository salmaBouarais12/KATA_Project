using System.ComponentModel.DataAnnotations;

namespace KATA.API.DTO.Requests;

public class PostPersonRequest
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    public bool IsValid()
    {
        if (string.IsNullOrEmpty(FirstName))
        {
            return false;
        }

        if (string.IsNullOrEmpty(LastName))
        {
            return false;
        }

        return true;
    }
}
