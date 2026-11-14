#nullable enable

using System;

namespace LangWatch.Models;

public partial class InstantEvalJudgeUsageBilling
{
    public string OrganizationId { get; set; } = null!;

    public bool UsageBilled { get; set; }

    public DateTime OccurredAt { get; set; }

    public bool FromCatchUp { get; set; }
}
