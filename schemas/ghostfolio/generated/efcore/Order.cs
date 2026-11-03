#nullable enable

using System;
using System.Collections.Generic;

namespace Ghostfolio.Models;

public partial class Order
{
    public virtual Account? Account { get; set; }

    public string? AccountId { get; set; }

    public string? AccountUserId { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Currency { get; set; }

    public DateTime Date { get; set; }

    public double Fee { get; set; }

    public string Id { get; set; } = null!;

    public double Quantity { get; set; }

    public string SymbolProfileId { get; set; } = null!;

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public Type Type { get; set; }

    public double UnitPrice { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual SymbolProfile SymbolProfile { get; set; } = null!;
}
