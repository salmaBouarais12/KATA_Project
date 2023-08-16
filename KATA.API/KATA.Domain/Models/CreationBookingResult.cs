namespace KATA.Domain.Models;

public class CreationBookingResult
{
    public Booking Booking { get; set; }
    public IEnumerable<Slot> ListOfReservation { get; set; } = new List<Slot>();
    public List<string> ErrorMsg { get; set; } = new List<string>();


}
