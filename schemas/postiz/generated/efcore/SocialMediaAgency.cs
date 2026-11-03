#nullable enable

using System;
using System.Collections.Generic;

namespace Postiz.Models;

public partial class SocialMediaAgency
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? LogoId { get; set; }

    public string? Website { get; set; }

    public string? Slug { get; set; }

    public string? Facebook { get; set; }

    public string? Instagram { get; set; }

    public string? Twitter { get; set; }

    public string? LinkedIn { get; set; }

    public string? Youtube { get; set; }

    public string? Tiktok { get; set; }

    public string? OtherSocialMedia { get; set; }

    public string ShortDescription { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool Approved { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Media? Logo { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<SocialMediaAgencyNiche> Niches { get; set; } = new List<SocialMediaAgencyNiche>();
}
