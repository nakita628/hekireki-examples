#nullable enable

using System;
using System.Collections.Generic;

namespace Jitsu.Models;

public partial class FunctionsServer
{
    public string WorkspaceId { get; set; } = null!;

    public string Class { get; set; } = null!;

    public string DeploymentId { get; set; } = null!;

    public List<string>? Connections { get; set; }

    public List<string>? EmptyConnections { get; set; }

    public List<string>? ProfileBuilders { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
