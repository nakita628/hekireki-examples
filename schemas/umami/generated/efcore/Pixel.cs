#nullable enable

using System;

namespace Umami.Models;

public partial class Pixel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public Guid? UserId { get; set; }

    public Guid? TeamId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual User? User { get; set; }

    public virtual Team? Team { get; set; }
}
