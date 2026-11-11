#nullable enable

using System;

namespace Trigger.Models;

public partial class TrustedDeployEnvironment
{
    public string Id { get; set; } = null!;

    public virtual TrustedDeployRepository TrustedDeployRepository { get; set; } = null!;

    public string TrustedDeployRepositoryId { get; set; } = null!;

    public virtual RuntimeEnvironment RuntimeEnvironment { get; set; } = null!;

    public string RuntimeEnvironmentId { get; set; } = null!;

    public bool Enabled { get; set; } = false;

    public TrustedDeployScope Scope { get; set; }

    public string? ScopeValue { get; set; }

    public string AdditionalClaims { get; set; } = "{}";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
