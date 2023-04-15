using System;
using System.Collections.Generic;

namespace ASP_busstation.Models1;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int Price { get; set; }

    public string? PassengerPassport { get; set; }

    public int? VoyageFk { get; set; }

    public int? UserFk { get; set; }

    //public virtual User? UserFkNavigation { get; set; }

    public virtual Voyage? VoyageFkNavigation { get; set; }
}
