using KATA.Domain.Interfaces.Repositories;
using KATA.Domain.Interfaces.Sevices;
using KATA.Domain.Models;

namespace KATA.Domain.Services;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IPersonService _personService;
    private readonly IRoomService _roomService;

    public BookingService(IBookingRepository bookingRepository, IPersonService personService, IRoomService roomService)
    {
        _bookingRepository = bookingRepository;
        _personService = personService;
        _roomService = roomService;
    }

    public async Task<IEnumerable<Booking>> GetReservationsAsync()
    {
        return await _bookingRepository.GetReservationsAsync();
    }

    public async Task<Booking?> GetReservationByRoomIdAsync(int roomId)
    {
        return await _bookingRepository.GetReservationByRoomIdAsync(roomId);
    }

    public async Task<CreationBookingResult> AddReservationAsync(Booking reservation)
    {
        (string errorMsgRoom, bool isValidRoom) = await CheckRoom(reservation.RoomId);
        (string errorMsgPeople, bool isValidPerson) = await CheckPersone(reservation.PersonId);
        (string errorMsgDate, bool isValidDate) = CheckDate(reservation.StartSlot, reservation.EndSlot);
        if (!isValidRoom || !isValidPerson || !isValidDate)
        {
            var result = new CreationBookingResult();
            if (!isValidRoom)
            {
                result.ErrorMsg.Add(errorMsgRoom);
            }
            if (!isValidPerson)
            {
                result.ErrorMsg.Add(errorMsgPeople);
            }
            if (!isValidDate)
            {
                result.ErrorMsg.Add(errorMsgDate);
            }
            return result;
        }
        var nonDisponible = await VerificationOfReservation(reservation);
        if (nonDisponible)
        {
            SearchBooking searchBooking = new SearchBooking();
            searchBooking.RoomId = reservation.RoomId;
            searchBooking.Date = reservation.BookingDate;
            var currentReservations = await _bookingRepository.GetReservationByRommAndByDate(searchBooking);
            var creationBookingResult = new CreationBookingResult();
            var ListeDisponible = new List<Slot>();
            int startSlot = 0;
            for (int i = 0; i <= 23; i++)
            {
                foreach (var currentVersion in currentReservations)
                {
                    if (currentVersion.StartSlot == i)
                    {
                        if (i > 0)
                        {
                            var slot = new Slot(startSlot, i - 1);
                            ListeDisponible.Add(slot);
                            startSlot = currentVersion.EndSlot + 1;
                        }
                    }
                    else if (i == 23)
                    {
                        var slot = new Slot(startSlot, i);
                        ListeDisponible.Add(slot);
                    }
                }
            }
            creationBookingResult.ListOfReservation = ListeDisponible;
            return creationBookingResult;
        }
        var booking = await _bookingRepository.AddReservationAsync(reservation);

        var creationBookingResultBooking = new CreationBookingResult();
        creationBookingResultBooking.Booking = booking;
        return creationBookingResultBooking;
    }
    private async Task<bool> VerificationOfReservation(Booking reservation)
    {
        var getReservations = await _bookingRepository.GetReservationsAsync();
        bool resrvationNonDisponible = false;
        foreach (var checkresrvation in getReservations)
        {
            if (reservation.BookingDate.Date == checkresrvation.BookingDate.Date)
            {
                if (checkresrvation.RoomId == reservation.RoomId)
                {
                    if (checkresrvation.StartSlot >= reservation.StartSlot && checkresrvation.StartSlot <= reservation.EndSlot ||
                        checkresrvation.EndSlot >= reservation.StartSlot && checkresrvation.EndSlot <= reservation.EndSlot)

                    {
                        resrvationNonDisponible = true;
                    }
                }
            }
        }
        return resrvationNonDisponible;
    }

    private static (string errorMsgDate, bool isValidDate) CheckDate(int startSlot, int endSlot)
    {
        if (startSlot < 0 && endSlot > 23)
        {
            return ("Réservation non confirmer : StartSlot : " + startSlot + " et EndSlot : " + endSlot, false);
        }
        return (string.Empty, true);
    }

    private async Task<(string errorMsgRoom, bool isValidRoom)> CheckRoom(int roomId)
    {
        var room = await _roomService.GetRoomByIdAsync(roomId);
        if (room == null)
        {
            return (" Numéro de la chambre est incorrect  : " + roomId, false);
        }
        return (string.Empty, true);
    }

    private async Task<(string errorMsgPeople, bool isValidPerson)> CheckPersone(int personId)
    {
        var person = await _personService.GetPersonByIdAsync(personId);
        if (person == null)
        {
            return ("Id de la personne est incorrect :  " + personId, false);
        }
        return (string.Empty, true);
    }

    public async Task<IEnumerable<Booking>> GetReservationByRommAndByDate(SearchBooking searchBooking)
    {
        return await _bookingRepository.GetReservationByRommAndByDate(searchBooking);
    }

    public async Task<Booking> DeleteBookingsAsync(int id)
    {
        return await _bookingRepository.DeleteBookingsAsync(id);
    }
}
