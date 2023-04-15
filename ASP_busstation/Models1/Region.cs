using System;
using System.Collections.Generic;

namespace ASP_busstation.Models1;

public partial class Region
{
    public int RegionId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Settlement> Settlements { get; } = new List<Settlement>();
}
