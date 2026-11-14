#nullable enable

using System;
using System.Collections.Generic;

namespace LangWatch.Models;

public partial class ApiKey
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string LookupId { get; set; } = null!;

    public string HashedSecret { get; set; } = null!;

    public string PermissionMode { get; set; } = "all";

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public string? CreatedByUserId { get; set; }

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<RoleBinding> RoleBindings { get; set; } = new List<RoleBinding>();

    public DateTime? ExpiresAt { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public DateTime? RevokedAt { get; set; }

    public string? RevocationCause { get; set; }

    public string? ParentApiKeyId { get; set; }

    public string? IngestSourceType { get; set; }

    public string? IngestionTemplateId { get; set; }

    public string? CreatedByDeviceLabel { get; set; }

    public bool IsSystemManaged { get; set; } = false;

    public string? CreatedByBrowserSessionId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
