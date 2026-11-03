#nullable enable

using System;

namespace Umami.Models;

public partial class Board
{
    public Guid Id { get; set; }

    public string Type { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Parameters { get; set; } = null!;

    public Guid? UserId { get; set; }

    public Guid? TeamId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public virtual User? User { get; set; }

    public virtual Team? Team { get; set; }
}
