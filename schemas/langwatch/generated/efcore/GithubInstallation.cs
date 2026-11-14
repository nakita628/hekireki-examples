#nullable enable

using System;

namespace LangWatch.Models;

public partial class GithubInstallation
{
    public string Id { get; set; } = null!;

    public string InstallationId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string AccountLogin { get; set; } = null!;

    public string AccountType { get; set; } = null!;

    public string AccountId { get; set; } = null!;

    public string RepositorySelection { get; set; } = null!;

    public string? Repositories { get; set; }

    public DateTime? SuspendedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
