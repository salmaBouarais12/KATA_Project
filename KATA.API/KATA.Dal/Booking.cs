﻿using System;
using System.Collections.Generic;

namespace KATA.Dal;

public partial class Booking
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public int PersonId { get; set; }

    public DateTime BookingDate { get; set; }

    public int StartSlot { get; set; }

    public int EndSlot { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}