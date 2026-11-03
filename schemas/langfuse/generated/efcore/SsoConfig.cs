#nullable enable

using System;

namespace Langfuse.Models;

public partial class SsoConfig
{
    public string Domain { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string AuthProvider { get; set; } = null!;

    public string? AuthConfig { get; set; }
}
