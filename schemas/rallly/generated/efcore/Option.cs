#nullable enable

using System;
using System.Collections.Generic;

namespace Rallly.Models;

public partial class Option
{
    public string Id { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public int Duration { get; set; } = 0;

    public string PollId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();

    public virtual Poll Poll { get; set; } = null!;
}
