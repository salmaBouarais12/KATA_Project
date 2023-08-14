namespace KATA.API.DTO.Responses
{
    public record BookingResponse(int Id, int RoomId, int PersonId, DateTime BookingDate, int StartSlot, int EndSlot);
}
