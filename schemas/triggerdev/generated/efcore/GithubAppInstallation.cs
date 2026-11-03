#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class GithubAppInstallation
{
    public string Id { get; set; } = null!;

    public long AppInstallationId { get; set; }

    public long TargetId { get; set; }

    public string TargetType { get; set; } = null!;

    public string AccountHandle { get; set; } = null!;

    public string? Permissions { get; set; }

    public GithubRepositorySelection RepositorySelection { get; set; }

    public string? InstalledBy { get; set; }

    public string? InstalledByUserId { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual ICollection<GithubRepository> Repositories { get; set; } = new List<GithubRepository>();

    public DateTime? DeletedAt { get; set; }

    public DateTime? SuspendedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
