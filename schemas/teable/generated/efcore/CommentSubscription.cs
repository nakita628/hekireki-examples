#nullable enable

using System;

namespace Teable.Models;

public partial class CommentSubscription
{
    public string Id { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string RecordId { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTime { get; set; }
}
