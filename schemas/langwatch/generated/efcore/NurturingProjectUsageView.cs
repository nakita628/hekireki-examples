#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class NurturingProjectUsageView
{
    public string ProjectId { get; set; } = null!;

    public string Owner { get; set; } = null!;

    public string Folded { get; set; } = null!;

    public List<string>? AppliedEventIds { get; set; } = new List<string>();

    public string ProjectionVersion { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }
}
