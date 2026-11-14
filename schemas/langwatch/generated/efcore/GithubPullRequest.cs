#nullable enable

using System;

namespace LangWatch.Models;

public partial class GithubPullRequest
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string RepositoryHost { get; set; } = null!;

    public string RepositoryFullName { get; set; } = null!;

    public string HeadBranch { get; set; } = null!;

    public int PrNumber { get; set; }

    public string HtmlUrl { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string State { get; set; } = null!;

    public bool IsDraft { get; set; } = false;

    public string? AuthorLogin { get; set; }

    public DateTime PrCreatedAt { get; set; }

    public DateTime? PrClosedAt { get; set; }

    public DateTime? PrMergedAt { get; set; }

    public DateTime? PrUpdatedAt { get; set; }

    public DateTime MappedAt { get; set; }

    public DateTime LastCheckedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
