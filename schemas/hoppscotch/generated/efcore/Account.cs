#nullable enable

using System;

namespace Hoppscotch.Models;

public partial class Account
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string ProviderAccountId { get; set; } = null!;

    public string? ProviderRefreshToken { get; set; }

    public string? ProviderAccessToken { get; set; }

    public string? ProviderScope { get; set; }

    public DateTime LoggedIn { get; set; }

    public virtual User User { get; set; } = null!;
}
