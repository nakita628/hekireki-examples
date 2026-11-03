#nullable enable

using System;

namespace Postiz.Models;

public partial class GitHub
{
    public string Id { get; set; } = null!;

    public string? Login { get; set; }

    public string? Name { get; set; }

    public string Token { get; set; } = null!;

    public string? JobId { get; set; }

    public string OrganizationId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;
}
