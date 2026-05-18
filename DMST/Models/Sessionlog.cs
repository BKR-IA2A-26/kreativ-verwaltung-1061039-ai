using System;
using System.Collections.Generic;

namespace DMST.Models;

public partial class Sessionlog
{
    public int SessionId { get; set; }

    public int CampaignId { get; set; }

    public DateOnly? Date { get; set; }

    public string? Notes { get; set; }

    public string? Summary { get; set; }

    public virtual Campaign Campaign { get; set; } = null!;
}
