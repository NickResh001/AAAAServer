using System;
using System.Collections.Generic;

namespace ASP_busstation.Models1;

public partial class Transport
{
    public int TransportId { get; set; }

    public int SeatsCount { get; set; }

    public string CarNumber { get; set; } = null!;

    public int TransportBrandFk { get; set; }

    public int? TransportCategoryFk { get; set; }

    public virtual TransportBrand TransportBrandFkNavigation { get; set; } = null!;

    public virtual TransportCategory? TransportCategoryFkNavigation { get; set; }

    public virtual ICollection<Voyage> Voyages { get; } = new List<Voyage>();
}
