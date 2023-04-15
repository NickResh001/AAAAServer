using System;
using System.Collections.Generic;

namespace ASP_busstation.Models1;

public partial class Voyage
{
    public int VoyageId { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime AllowTime { get; set; }

    public int Platform { get; set; }

    public int? TotalSeats { get; set; }

    public int? FreeSeats { get; set; }

    public string? DriverPassport { get; set; }

    public int? RouteFk { get; set; }

    public int? TransportFk { get; set; }

    public int? VoyageStatusFk { get; set; }

    public int? DriverFk { get; set; }

    public virtual Driver? DriverFkNavigation { get; set; }

    public virtual BusRoute? RouteFkNavigation { get; set; }

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();

    public virtual Transport? TransportFkNavigation { get; set; }

    public virtual VoyageStatus? VoyageStatusFkNavigation { get; set; }
}
