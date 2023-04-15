using System;
using System.Collections.Generic;

namespace ASP_busstation.DTOs
{
    public class SettlementDTO
    {
        public int SettlementDTOId { get; set; }
        public string Title { get; set; } = null!;
        public int RegionFK { get; set; }
        public string? RegionTitle { get; set; }

    }
}
