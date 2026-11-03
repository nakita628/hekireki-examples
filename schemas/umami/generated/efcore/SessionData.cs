#nullable enable

using System;

namespace Umami.Models;

public partial class SessionData
{
    public Guid Id { get; set; }

    public Guid WebsiteId { get; set; }

    public Guid SessionId { get; set; }

    public string DataKey { get; set; } = null!;

    public string? StringValue { get; set; }

    public decimal? NumberValue { get; set; }

    public DateTime? DateValue { get; set; }

    public int DataType { get; set; }

    public string? DistinctId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Website Website { get; set; } = null!;

    public virtual Session Session { get; set; } = null!;
}
