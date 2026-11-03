#nullable enable

using System;

namespace Postiz.Models;

public partial class AutoPost
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public bool OnSlot { get; set; }

    public bool SyncLast { get; set; }

    public string Url { get; set; } = null!;

    public string LastUrl { get; set; } = null!;

    public bool Active { get; set; }

    public bool AddPicture { get; set; }

    public bool GenerateContent { get; set; }

    public string Integrations { get; set; } = null!;

    public DateTime? DeletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;
}
