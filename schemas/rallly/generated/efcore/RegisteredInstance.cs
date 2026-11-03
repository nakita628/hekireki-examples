#nullable enable

using System;

namespace Rallly.Models;

public partial class RegisteredInstance
{
    public string Id { get; set; } = null!;

    public string InstanceId { get; set; } = null!;

    public string Version { get; set; } = null!;

    public DateTime FirstSeenAt { get; set; }

    public DateTime LastSeenAt { get; set; }
}
