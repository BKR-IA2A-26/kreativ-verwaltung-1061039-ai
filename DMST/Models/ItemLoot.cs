using System;
using System.Collections.Generic;

namespace DMST.Models;

public partial class ItemLoot
{
    public int ItemId { get; set; }

    public string Name { get; set; } = null!;

    public string? Attributes { get; set; }

    public decimal? Weight { get; set; }

    public string? Rarity { get; set; }

    public int PlaceId { get; set; }

    public virtual Place Place { get; set; } = null!;
}
