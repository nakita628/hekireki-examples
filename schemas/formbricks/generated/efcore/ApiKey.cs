#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class ApiKey
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public string Label { get; set; } = null!;

    public string HashedKey { get; set; } = null!;

    public string? LookupHash { get; set; }

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<ApiKeyWorkspace> ApiKeyWorkspaces { get; set; } = new List<ApiKeyWorkspace>();

    public string OrganizationAccess { get; set; } = "{}";
}
