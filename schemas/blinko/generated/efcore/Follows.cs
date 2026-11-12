#nullable enable

using System;

namespace Blinko.Models;

public partial class Follows
{
    public int Id { get; set; }

    public string? SiteName { get; set; }

    public string SiteUrl { get; set; } = null!;

    public string? SiteAvatar { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string FollowType { get; set; } = "following";

    public int AccountId { get; set; }

    public virtual Accounts Account { get; set; } = null!;
}
