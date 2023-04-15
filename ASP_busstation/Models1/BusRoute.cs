using System;
using System.Collections.Generic;

namespace ASP_busstation.Models1;

public partial class BusRoute
{
    public int BusRouteId { get; set; }

    public double PriceCoef { get; set; }

    public double Length { get; set; }

    public virtual ICollection<BusShelter> BusShelters { get; } = new List<BusShelter>();

    public virtual ICollection<Voyage> Voyages { get; } = new List<Voyage>();
}
