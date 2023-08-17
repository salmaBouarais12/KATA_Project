using KATA.Domain.Models;

namespace KATA.API.DTO.Responses
{
    public class ReservationResponse
    {
        public Booking? Reservation { get; set; }
        public IEnumerable<SlotDTO> ListesCreneux { get; set; } = new List<SlotDTO>();
        public List<String> Message { get; set; } = new List<string>();
    }
}
