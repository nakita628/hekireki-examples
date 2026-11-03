#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class WebCalloutEndpoint
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ProjectId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string Name { get; set; } = "Default";

    public string Url { get; set; } = null!;

    public bool Enabled { get; set; } = true;

    public string ToastMessage { get; set; } = "Callout sent";

    public string? RequestHeaders { get; set; }

    public List<string>? RequestHeaderKeys { get; set; } = new List<string>();
}
