using System;
using System.Collections.Generic;

namespace ASP_busstation.Models1;

public partial class VoyageStatus
{
    public int VoyageStatusId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Voyage> Voyages { get; } = new List<Voyage>();
}
