using System;
using System.Collections.Generic;

namespace ASP_busstation.Models1;

public partial class Settlement
{
    public int SettlementId { get; set; }

    public string Title { get; set; } = null!;

    public int RegionFk { get; set; }

    public virtual ICollection<BusShelter> BusShelters { get; } = new List<BusShelter>();

    public virtual Region RegionFkNavigation { get; set; } = null!;
}
