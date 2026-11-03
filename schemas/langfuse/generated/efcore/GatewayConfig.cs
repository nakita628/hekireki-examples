#nullable enable

using System;

namespace Langfuse.Models;

public partial class GatewayConfig
{
    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string? DefaultIngestionProjectId { get; set; }

    public virtual Project? DefaultIngestionProject { get; set; }

    public GatewayIngestionMode IngestionMode { get; set; } = GatewayIngestionMode.Usage;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
