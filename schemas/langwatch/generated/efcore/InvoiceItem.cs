#nullable enable

using System;

namespace LangWatch.Models;

public partial class InvoiceItem
{
    public string Id { get; set; } = null!;

    public string InvoiceId { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
