#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Domain
{
    public string Id { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public virtual User? User { get; set; }

    public string? UserId { get; set; }

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public bool Verified { get; set; } = false;

    public bool IsDefault { get; set; } = false;

    public string? RedirectUrl { get; set; }

    public DateTime LastChecked { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();
}
