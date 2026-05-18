using System;
using System.Collections.Generic;

namespace DMST.Models;

public partial class Place
{
    public int PlaceId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Type { get; set; }

    public int CampaignId { get; set; }

    public virtual Campaign Campaign { get; set; } = null!;

    public virtual ICollection<ItemLoot> ItemLoots { get; set; } = new List<ItemLoot>();

    public virtual ICollection<Npc> Npcs { get; set; } = new List<Npc>();
}
