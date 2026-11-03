#nullable enable

using System;

namespace Postiz.Models;

public partial class Signatures
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public bool AutoAdd { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;
}
