using System;
using System.Collections.Generic;

namespace DMST.Models;

public partial class Npc
{
    public int NpcId { get; set; }

    public string Name { get; set; } = null!;

    public string? Race { get; set; }

    public string? Mindset { get; set; }

    public string? Notes { get; set; }

    public int CampaignId { get; set; }

    public int PlaceId { get; set; }

    public virtual Campaign Campaign { get; set; } = null!;

    public virtual Place Place { get; set; } = null!;
}
