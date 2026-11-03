#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraChannelConfig
{
    public string Platform { get; set; } = null!;

    public string Data { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public DateTime? UpdatedAtZ { get; set; } = DateTime.UtcNow;
}
