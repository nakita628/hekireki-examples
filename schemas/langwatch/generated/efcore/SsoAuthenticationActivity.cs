#nullable enable

using System;

namespace LangWatch.Models;

public partial class SsoAuthenticationActivity
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public DateTime AuthenticatedAt { get; set; }

    public string? ProviderAccountId { get; set; }
}
