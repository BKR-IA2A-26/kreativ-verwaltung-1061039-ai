using System;
using System.Collections.Generic;

namespace DMST.Models;

public partial class Class
{
    public int ClassId { get; set; }

    public string Name { get; set; } = null!;

    public string? HpDice { get; set; }

    public string? SpellCast { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<CharacterClassRel> CharacterClassRels { get; set; } = new List<CharacterClassRel>();
}
