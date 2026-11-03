#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class TaskList
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? OrderIndex { get; set; }

    public string DataroomId { get; set; } = null!;

    public virtual Dataroom Dataroom { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
