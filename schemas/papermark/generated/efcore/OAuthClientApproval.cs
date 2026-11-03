#nullable enable

using System;

namespace Papermark.Models;

public partial class OAuthClientApproval
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ClientId { get; set; } = null!;

    public DateTime ApprovedAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public string? ClientMetadataHash { get; set; }
}
