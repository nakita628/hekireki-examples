#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class RuntimeEnvironmentSession
{
    public string Id { get; set; } = null!;

    public string IpAddress { get; set; } = null!;

    public virtual RuntimeEnvironment Environment { get; set; } = null!;

    public string EnvironmentId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DisconnectedAt { get; set; }

    public virtual ICollection<RuntimeEnvironment> CurrentEnvironments { get; set; } = new List<RuntimeEnvironment>();
}
