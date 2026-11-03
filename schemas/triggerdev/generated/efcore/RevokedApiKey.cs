#nullable enable

using System;

namespace Trigger.Models;

public partial class RevokedApiKey
{
    public string Id { get; set; } = null!;

    public string ApiKey { get; set; } = null!;

    public virtual RuntimeEnvironment RuntimeEnvironment { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }
}
