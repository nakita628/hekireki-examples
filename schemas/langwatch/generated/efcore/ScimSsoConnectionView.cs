#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class ScimSsoConnectionView
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Folded { get; set; } = null!;

    public List<string>? AppliedEventIds { get; set; } = new List<string>();

    public string ProjectionVersion { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
