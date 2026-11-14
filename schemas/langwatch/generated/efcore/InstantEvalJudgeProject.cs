#nullable enable

using System;

namespace LangWatch.Models;

public partial class InstantEvalJudgeProject
{
    public string ProjectId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
