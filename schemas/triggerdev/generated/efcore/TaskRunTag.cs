#nullable enable

using System;

namespace Trigger.Models;

public partial class TaskRunTag
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
