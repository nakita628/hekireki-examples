#nullable enable

using System;

namespace LangWatch.Models;

public partial class GithubBranchPullRequestCheck
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string RepositoryHost { get; set; } = null!;

    public string RepositoryFullName { get; set; } = null!;

    public string HeadBranch { get; set; } = null!;

    public DateTime LastCheckedAt { get; set; }

    public int PrCount { get; set; } = 0;

    public DateTime? NotFoundAt { get; set; }

    public DateTime? RecheckAfter { get; set; }

    public int Attempts { get; set; } = 0;

    public DateTime LastRequestedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
