using System.ComponentModel.DataAnnotations;

namespace KATA.API.DTO.Requests;

public class PostPersonRequest
{
    [Required(ErrorMessage = "Le champ est vide")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le champ est vide")]
    public string LastName { get; set; } = string.Empty;
}
