#nullable enable

using System;

namespace LangWatch.Models;

public partial class ScimUserResource
{
    public string OrganizationId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string? Name { get; set; }

    public bool Active { get; set; } = true;

    public DateTime? DeletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
