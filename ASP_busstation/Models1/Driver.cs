using System;
using System.Collections.Generic;

namespace ASP_busstation.Models1;

public partial class Driver
{
    public int DriverId { get; set; }

    public string Fio { get; set; } = null!;

    public string Passport { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string DrivingLicense { get; set; } = null!;

    public virtual ICollection<Voyage> Voyages { get; } = new List<Voyage>();
}
