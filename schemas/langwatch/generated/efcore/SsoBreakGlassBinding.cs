#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class SsoBreakGlassBinding
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string GrantedByUserId { get; set; } = null!;

    public DateTime GrantedAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime? SupersededAt { get; set; }

    public string? RenewedFromId { get; set; }

    public List<int>? WarnedDays { get; set; }

    public DateTime CreatedAt { get; set; }
}
