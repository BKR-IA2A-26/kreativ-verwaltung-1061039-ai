using System;
using System.Collections.Generic;

namespace DMST.Models;

public partial class Quest
{
    public int QuestId { get; set; }

    public string Title { get; set; } = null!;

    public string? Reward { get; set; }

    public string? Requirement { get; set; }

    public int CampaignId { get; set; }

    public virtual Campaign Campaign { get; set; } = null!;
}
