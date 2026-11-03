#nullable enable

using System;

namespace Papermark.Models;

public partial class InstalledIntegration
{
    public string Id { get; set; } = null!;

    public string? Credentials { get; set; }

    public string? Configuration { get; set; }

    public bool Enabled { get; set; } = true;

    public string IntegrationId { get; set; } = null!;

    public virtual Integration Integration { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
