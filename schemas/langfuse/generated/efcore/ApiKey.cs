#nullable enable

using System;
using System.Collections.Generic;

namespace Langfuse.Models;

public partial class ApiKey
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string? Note { get; set; }

    public string PublicKey { get; set; } = null!;

    public string HashedSecretKey { get; set; } = null!;

    public string? FastHashedSecretKey { get; set; }

    public string DisplaySecretKey { get; set; } = null!;

    public DateTime? LastUsedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public bool IsInAppAgentKey { get; set; } = false;

    public string? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public string? OrgId { get; set; }

    public virtual Organization? Organization { get; set; }

    public ApiKeyScope Scope { get; set; } = ApiKeyScope.Project;

    public string? CreatedByUserId { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public string? CreatedByApiKeyId { get; set; }

    public virtual ApiKey? CreatedByApiKey { get; set; }

    public virtual ICollection<ApiKey> ApiKeysCreated { get; set; } = new List<ApiKey>();

    public virtual GatewayApiKeyAssociation? GatewayAssociation { get; set; }

    public virtual ICollection<RoleAssignment> RoleAssignments { get; set; } = new List<RoleAssignment>();
}
