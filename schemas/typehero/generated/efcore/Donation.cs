#nullable enable

using System;

namespace TypeHero.Models;

public partial class Donation
{
    public int Id { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public int Amount { get; set; }

    public string StripeCheckoutSessionId { get; set; } = null!;

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
}
