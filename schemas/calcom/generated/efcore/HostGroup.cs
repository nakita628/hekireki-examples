#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class HostGroup
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Host> Hosts { get; set; } = new List<Host>();

    public virtual EventType? EventType { get; set; }

    public int? EventTypeId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
