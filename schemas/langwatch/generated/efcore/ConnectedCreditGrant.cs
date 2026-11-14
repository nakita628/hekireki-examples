#nullable enable

using System;

namespace LangWatch.Models;

public partial class ConnectedCreditGrant
{
    public string Id { get; set; } = null!;

    public string AccountId { get; set; } = null!;

    public string StripeCreditGrantId { get; set; } = null!;

    public int AmountUsdCents { get; set; }

    public ConnectedCreditGrantKind Kind { get; set; }

    public DateTime TermEndsAt { get; set; }

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
