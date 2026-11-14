#nullable enable

using System;

namespace LangWatch.Models;

public partial class IdentityMatch
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string DiscoveredPersonId { get; set; } = null!;

    public string? UserId { get; set; }

    public string EvidenceKind { get; set; } = null!;

    public DateTime ValidFrom { get; set; }

    public DateTime? ValidTo { get; set; }

    public DateTime CreatedAt { get; set; }
}
