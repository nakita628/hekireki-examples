#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class EnvironmentVariable
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public string Key { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<EnvironmentVariableValue> Values { get; set; } = new List<EnvironmentVariableValue>();
}
