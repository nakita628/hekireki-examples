#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMProxyWorkerHeartbeat
{
    public string WorkerId { get; set; } = null!;

    public string Hostname { get; set; } = null!;

    public DateTime StartedAt { get; set; }

    public DateTime LastHeartbeatAt { get; set; }
}
