#nullable enable

using System;

namespace LangWatch.Models;

public partial class IdempotencyReceipt
{
    public string Id { get; set; } = null!;

    public string ScopeId { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string ClaimId { get; set; } = null!;

    public string RequestFingerprint { get; set; } = null!;

    public int? ResponseStatus { get; set; }

    public string? ResponseBody { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime HeartbeatAt { get; set; }

    public DateTime ExpiresAt { get; set; }
}
