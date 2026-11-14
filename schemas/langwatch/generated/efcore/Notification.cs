#nullable enable

using System;

namespace LangWatch.Models;

public partial class Notification
{
    public string Id { get; set; } = null!;

    public string? OrganizationId { get; set; }

    public virtual Organization? Organization { get; set; }

    public string? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public string Metadata { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime SentAt { get; set; }
}
