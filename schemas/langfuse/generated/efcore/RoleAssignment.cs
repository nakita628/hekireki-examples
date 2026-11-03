#nullable enable

using System;

namespace Langfuse.Models;

public partial class RoleAssignment
{
    public string Id { get; set; } = null!;

    public string OwnerId { get; set; } = null!;

    public string? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public string OrgId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string PrincipalId { get; set; } = null!;

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public string? ApiKeyId { get; set; }

    public virtual ApiKey? ApiKey { get; set; }

    public string RoleId { get; set; } = null!;

    public SystemRole SystemRole { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
