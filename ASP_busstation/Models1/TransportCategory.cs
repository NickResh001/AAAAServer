using System;
using System.Collections.Generic;

namespace ASP_busstation.Models1;

public partial class TransportCategory
{
    public int TransportCategoryId { get; set; }

    public string Title { get; set; } = null!;

    public virtual ICollection<Transport> Transports { get; } = new List<Transport>();
}
