using System;
using System.Collections.Generic;

namespace ASP_busstation.Models1;

public partial class BusShelter
{
    public int BusShelterId { get; set; }

    public int SettlementFk { get; set; }

    public int BusRouteFk { get; set; }

    public double RouteShare { get; set; }

    public int SeqNumber { get; set; }

    public virtual BusRoute BusRouteFkNavigation { get; set; } = null!;

    public virtual Settlement SettlementFkNavigation { get; set; } = null!;
}
