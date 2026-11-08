#nullable enable

using System;

namespace Documenso.Models;

public partial class OrganisationClaim
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? OriginalSubscriptionClaimId { get; set; }

    public virtual Organisation? Organisation { get; set; }

    public int TeamCount { get; set; }

    public int MemberCount { get; set; }

    public int EnvelopeItemCount { get; set; }

    public int RecipientCount { get; set; }

    public int EnvelopeContentCount { get; set; }

    public int EnvelopeContentImageCount { get; set; }

    public string Flags { get; set; } = null!;

    public string DocumentRateLimits { get; set; } = null!;

    public int? DocumentQuota { get; set; }

    public string EmailRateLimits { get; set; } = null!;

    public int? EmailQuota { get; set; }

    public string ApiRateLimits { get; set; } = null!;

    public int? ApiQuota { get; set; }

    public string? EmailTransportId { get; set; }

    public virtual EmailTransport? EmailTransport { get; set; }
}
