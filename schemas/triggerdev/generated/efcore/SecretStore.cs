#nullable enable

using System;

namespace Trigger.Models;

public partial class SecretStore
{
    public string Key { get; set; } = null!;

    public string Value { get; set; } = null!;

    public string Version { get; set; } = "1";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
