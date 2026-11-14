#nullable enable

using System;

namespace LangWatch.Models;

public partial class IngestionTemplate
{
    public string Id { get; set; } = null!;

    public string? OrganizationId { get; set; }

    public virtual Organization? Organization { get; set; }

    public string Slug { get; set; } = null!;

    public string SourceType { get; set; } = null!;

    public string DisplayName { get; set; } = null!;

    public string? Description { get; set; }

    public string? IconAsset { get; set; }

    public string? CredentialSchema { get; set; }

    public string OttlRules { get; set; } = "";

    public bool PlatformPublished { get; set; } = false;

    public bool Enabled { get; set; } = true;

    public DateTime? ArchivedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? CreatedById { get; set; }

    public string? UpdatedById { get; set; }
}
