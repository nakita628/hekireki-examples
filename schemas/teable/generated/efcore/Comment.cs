#nullable enable

using System;

namespace Teable.Models;

public partial class Comment
{
    public string Id { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string RecordId { get; set; } = null!;

    public string? QuoteId { get; set; }

    public string? Content { get; set; }

    public string? Reaction { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? LastModifiedTime { get; set; }
}
