#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class ActivationCode
{
    public string Id { get; set; } = null!;

    public string CodeHash { get; set; } = null!;

    public string CodeHint { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string OrganizationName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PlanType { get; set; } = null!;

    public int MaxMembers { get; set; }

    public int? MaxMembersLite { get; set; } = 0;

    public bool LiteSeatsUnlimited { get; set; } = false;

    public int? MaxMembersDeveloper { get; set; }

    public int LicenseTermDays { get; set; }

    public List<string>? Services { get; set; } = new List<string>();

    public DateTime ExpiresAt { get; set; }

    public bool Reusable { get; set; } = false;

    public DateTime? RedeemedAt { get; set; }

    public string? RedeemedByInstanceId { get; set; }

    public string? IssuedLicenseId { get; set; }

    public int RedemptionCount { get; set; } = 0;

    public DateTime? RevokedAt { get; set; }

    public string? RevokedById { get; set; }

    public string? CreatedById { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
