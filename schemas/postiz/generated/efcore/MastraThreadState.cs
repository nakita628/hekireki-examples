#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraThreadState
{
    public string ThreadId { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Value { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAtZ { get; set; } = DateTime.UtcNow;
}
