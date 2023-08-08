using System.ComponentModel.DataAnnotations;

namespace KATA.API.DTO.Requests;

public class PostPersonRequest
{
    [Required(ErrorMessage = "Le champ est vide")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le champ est vide")]
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
