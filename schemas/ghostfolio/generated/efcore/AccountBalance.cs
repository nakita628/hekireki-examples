#nullable enable

using System;

namespace Ghostfolio.Models;

public partial class AccountBalance
{
    public virtual Account Account { get; set; } = null!;

    public string AccountId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime Date { get; set; }

    public string Id { get; set; } = null!;

    public DateTime UpdatedAt { get; set; }

    public string UserId { get; set; } = null!;

    public double Value { get; set; }
}
