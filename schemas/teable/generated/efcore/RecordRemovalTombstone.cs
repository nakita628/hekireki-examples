#nullable enable

using System;

namespace Teable.Models;

public partial class RecordRemovalTombstone
{
    public string Id { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string RecordId { get; set; } = null!;

    public string Type { get; set; } = null!;

    public DateTime CreatedTime { get; set; }
}
