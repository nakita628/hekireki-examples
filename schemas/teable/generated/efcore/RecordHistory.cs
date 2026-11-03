#nullable enable

using System;

namespace Teable.Models;

public partial class RecordHistory
{
    public string Id { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string RecordId { get; set; } = null!;

    public string FieldId { get; set; } = null!;

    public string Before { get; set; } = null!;

    public string After { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;
}
