#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class TrustedDeployRepository
{
    public string Id { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string Issuer { get; set; } = "https://token.actions.githubusercontent.com";

    public string RepositoryOwner { get; set; } = null!;

    public string RepositoryName { get; set; } = null!;

    public string? RepositoryId { get; set; }

    public string? RepositoryOwnerId { get; set; }

    public virtual ICollection<TrustedDeployEnvironment> Environments { get; set; } = new List<TrustedDeployEnvironment>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
