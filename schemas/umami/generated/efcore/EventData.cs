#nullable enable

using System;

namespace Umami.Models;

public partial class EventData
{
    public Guid Id { get; set; }

    public Guid WebsiteId { get; set; }

    public Guid WebsiteEventId { get; set; }

    public string DataKey { get; set; } = null!;

    public string? StringValue { get; set; }

    public decimal? NumberValue { get; set; }

    public DateTime? DateValue { get; set; }

    public int DataType { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Website Website { get; set; } = null!;

    public virtual WebsiteEvent WebsiteEvent { get; set; } = null!;
}
