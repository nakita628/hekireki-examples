#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class Invoice
{
    public string Id { get; set; } = null!;

    public string SubscriptionId { get; set; } = null!;

    public double TotalAmount { get; set; }

    public string Currency { get; set; } = null!;

    public DateTime DueDate { get; set; }

    public bool Paid { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Subscription Subscription { get; set; } = null!;

    public virtual ICollection<InvoiceItem> LineItems { get; set; } = new List<InvoiceItem>();
}
