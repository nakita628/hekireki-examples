#nullable enable

using System;

namespace AnythingLLM.Models;

public partial class ExternalCommunicationConnectors
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Config { get; set; } = "{}";

    public bool Active { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }
}
