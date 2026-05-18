using System;
using System.Collections.Generic;

namespace DMST.Models;

public partial class CharacterClassRel
{
    public int CharId { get; set; }

    public int ClassId { get; set; }

    public int? Lvl { get; set; }

    public virtual Playercharacter Char { get; set; } = null!;

    public virtual Class Class { get; set; } = null!;
}
