#nullable enable

using System;
using System.Collections.Generic;

namespace Postiz.Models;

public partial class Media
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? OriginalName { get; set; }

    public string Path { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int FileSize { get; set; } = 0;

    public string Type { get; set; } = "image";

    public string? Thumbnail { get; set; }

    public string? Alt { get; set; }

    public int? ThumbnailTimestamp { get; set; }

    public string Status { get; set; } = "ready";

    public string? ProcessingError { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<SocialMediaAgency> Agencies { get; set; } = new List<SocialMediaAgency>();

    public virtual ICollection<User> UserPicture { get; set; } = new List<User>();

    public virtual ICollection<OAuthApp> OauthApps { get; set; } = new List<OAuthApp>();
}
