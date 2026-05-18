using System;
using System.Collections.Generic;

namespace DMST.Models;

public partial class Playercharacter
{
    public int CharId { get; set; }

    public string Name { get; set; } = null!;

    public string? Race { get; set; }

    public string? Spells { get; set; }

    public string? Weapons { get; set; }

    public string? Notes { get; set; }

    public int CampaignId { get; set; }

    public virtual Campaign Campaign { get; set; } = null!;

    public virtual ICollection<CharacterClassRel> CharacterClassRels { get; set; } = new List<CharacterClassRel>();
}
