#nullable enable

using System;
using System.Collections.Generic;

namespace LiteLLM.Models;

public partial class LiteLLMPolicyTable
{
    public string PolicyId { get; set; } = null!;

    public string PolicyName { get; set; } = null!;

    public int VersionNumber { get; set; } = 1;

    public string VersionStatus { get; set; } = "production";

    public string? ParentVersionId { get; set; }

    public bool IsLatest { get; set; } = true;

    public DateTime? PublishedAt { get; set; }

    public DateTime? ProductionAt { get; set; }

    public string? Inherit { get; set; }

    public string? Description { get; set; }

    public List<string>? GuardrailsAdd { get; set; } = new List<string>();

    public List<string>? GuardrailsRemove { get; set; } = new List<string>();

    public string? Condition { get; set; } = "{}";

    public string? Pipeline { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
