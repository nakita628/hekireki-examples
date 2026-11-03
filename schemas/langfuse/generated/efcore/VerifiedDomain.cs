#nullable enable

using System;

namespace Langfuse.Models;

public partial class VerifiedDomain
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string Domain { get; set; } = null!;

    public string VerificationToken { get; set; } = null!;

    public DateTime? VerifiedAt { get; set; }

    public string? CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
