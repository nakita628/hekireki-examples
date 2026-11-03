#nullable enable

using System;

namespace Dub.Models;

public partial class RegisteredDomain
{
    public string Id { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string? DomainId { get; set; }

    public DateTime? AutoRenewalDisabledAt { get; set; }

    public int RenewalFee { get; set; } = 1200;

    public DateTime ExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual Domain? Domain { get; set; }
}
