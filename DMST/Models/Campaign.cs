using System;
using System.Collections.Generic;

namespace DMST.Models;

public partial class Campaign
{
    public int CampaignId { get; set; }

    public string Name { get; set; } = null!;

    public DateOnly? StartDate { get; set; }

    public string? Setting { get; set; }

    public virtual ICollection<Npc> Npcs { get; set; } = new List<Npc>();

    public virtual ICollection<Place> Places { get; set; } = new List<Place>();

    public virtual ICollection<Playercharacter> Playercharacters { get; set; } = new List<Playercharacter>();

    public virtual ICollection<Quest> Quests { get; set; } = new List<Quest>();

    public virtual ICollection<Sessionlog> Sessionlogs { get; set; } = new List<Sessionlog>();
}
