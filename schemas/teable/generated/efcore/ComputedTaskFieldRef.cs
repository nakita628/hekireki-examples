#nullable enable

using System;

namespace Teable.Models;

public partial class ComputedTaskFieldRef
{
    public string TaskId { get; set; } = null!;

    public string FieldId { get; set; } = null!;

    public string TableId { get; set; } = null!;

    public string BaseId { get; set; } = null!;

    public bool WasProcessing { get; set; } = false;

    public DateTime CreatedAt { get; set; }
}
