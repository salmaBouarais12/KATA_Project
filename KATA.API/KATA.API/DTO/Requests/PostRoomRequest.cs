using System.ComponentModel.DataAnnotations;

namespace KATA.API.DTO.Requests;

public class PostRoomRequest
{
    [Required(ErrorMessage = "Le champ est vide")]
    public string RoomName { get; set; } = string.Empty;
}
