#nullable enable

using System;

namespace Postiz.Models;

public partial class PayoutProblems
{
    public string Id { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string OrderId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? PostId { get; set; }

    public int Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Orders Order { get; set; } = null!;

    public virtual Post? Post { get; set; }

    public virtual User User { get; set; } = null!;
}
