#nullable enable

using System;

namespace LangWatch.Models;

public partial class InstantEvalJudgeSpend
{
    public string OrganizationId { get; set; } = null!;

    public string RequestId { get; set; } = null!;

    public long SpendNanoUsd { get; set; }

    public DateTime OccurredAt { get; set; }
}
