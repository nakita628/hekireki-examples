#nullable enable

using System;

namespace Umami.Models;

public partial class Share
{
    public Guid Id { get; set; }

    public Guid EntityId { get; set; }

    public string Name { get; set; } = null!;

    public int ShareType { get; set; }

    public string Slug { get; set; } = null!;

    public string Parameters { get; set; } = null!;

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }
}
