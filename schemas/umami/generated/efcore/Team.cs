#nullable enable

using System;
using System.Collections.Generic;

namespace Umami.Models;

public partial class Team
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? AccessCode { get; set; }

    public string? LogoUrl { get; set; }

    public bool TwoFactorRequired { get; set; } = false;

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Website> Websites { get; set; } = new List<Website>();

    public virtual ICollection<TeamUser> Members { get; set; } = new List<TeamUser>();

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual ICollection<Pixel> Pixels { get; set; } = new List<Pixel>();

    public virtual ICollection<Board> Boards { get; set; } = new List<Board>();
}
