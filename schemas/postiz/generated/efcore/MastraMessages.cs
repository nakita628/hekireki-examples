#nullable enable

using System;

namespace Postiz.Models;

public partial class MastraMessages
{
    public string Id { get; set; } = null!;

    public string ThreadId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? ResourceId { get; set; }

    public DateTime? CreatedAtZ { get; set; } = DateTime.UtcNow;
}
