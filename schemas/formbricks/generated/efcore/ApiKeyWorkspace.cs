#nullable enable

using System;

namespace Formbricks.Models;

public partial class ApiKeyWorkspace
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ApiKeyId { get; set; } = null!;

    public virtual ApiKey ApiKey { get; set; } = null!;

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public ApiKeyPermission Permission { get; set; }
}
