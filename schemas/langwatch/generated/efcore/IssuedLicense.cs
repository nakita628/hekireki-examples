#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class IssuedLicense
{
    public string Id { get; set; } = null!;

    public string LicenseId { get; set; } = null!;

    public string TokenHash { get; set; } = null!;

    public string? OrganizationId { get; set; }

    public virtual Organization? Organization { get; set; }

    public string OrganizationName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PlanType { get; set; } = null!;

    public int MaxMembers { get; set; }

    public int? MaxMembersLite { get; set; }

    public bool LiteSeatsUnlimited { get; set; } = false;

    public int? MaxMembersDeveloper { get; set; }

    public DateTime IssuedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public IssuedLicenseSource Source { get; set; }

    public string? IssuedById { get; set; }

    public DateTime? RevokedAt { get; set; }

    public string? RevokedById { get; set; }

    public string? RevokedReason { get; set; }

    public DateTime? SupersededAt { get; set; }

    public string? ReplacesId { get; set; }

    public string? PendingDeliveryLicense { get; set; }

    public List<string>? Services { get; set; } = new List<string>();

    public int? SeatRateCents { get; set; }

    public Currency? SeatCurrency { get; set; }

    public int CommitUsdCents { get; set; } = 0;

    public bool OverageEnabled { get; set; } = false;

    public int? OverageMaxUsdCents { get; set; }

    public string? InstanceId { get; set; }

    public DateTime? InstanceBoundAt { get; set; }

    public DateTime? LastSyncAt { get; set; }

    public string? LastSyncVersion { get; set; }

    public int? ReportedMembers { get; set; }

    public int? ReportedMembersLite { get; set; }

    public string? VirtualKeyId { get; set; }

    public int? SeatsRaisedFrom { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
