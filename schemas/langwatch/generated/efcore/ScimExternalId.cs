#nullable enable

using System;

namespace LangWatch.Models;

public partial class ScimExternalId
{
    public string OrganizationId { get; set; } = null!;

    public string Id { get; set; } = null!;

    public string ConnectionId { get; set; } = null!;

    public string ExternalId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
