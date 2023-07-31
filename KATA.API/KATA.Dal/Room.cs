using System;
using System.Collections.Generic;

namespace KATA.Dal;

public partial class Room
{
    public int Id { get; set; }

    public string RoomName { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
