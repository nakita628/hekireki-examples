#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class GithubRepository
{
    public string Id { get; set; } = null!;

    public long GithubId { get; set; }

    public string Name { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string HtmlUrl { get; set; } = null!;

    public bool Private { get; set; }

    public string DefaultBranch { get; set; } = null!;

    public virtual GithubAppInstallation Installation { get; set; } = null!;

    public string InstallationId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<ConnectedGithubRepository> ConnectedGithubRepository { get; set; } = new List<ConnectedGithubRepository>();
}
