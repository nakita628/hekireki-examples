#nullable enable

using System;

namespace LiteLLM.Models;

public partial class LiteLLMManagedObjectTable
{
    public string Id { get; set; } = null!;

    public string UnifiedObjectId { get; set; } = null!;

    public string ModelObjectId { get; set; } = null!;

    public string FileObject { get; set; } = null!;

    public string FilePurpose { get; set; } = null!;

    public string? Status { get; set; }

    public bool BatchProcessed { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? TeamId { get; set; }

    public string? OrgId { get; set; }

    public string? ApiKey { get; set; }

    public string? RequestTags { get; set; } = "[]";

    public string? EndUserId { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}
