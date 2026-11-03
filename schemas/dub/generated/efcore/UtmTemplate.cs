#nullable enable

using System;

namespace Dub.Models;

public partial class UtmTemplate
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? UtmSource { get; set; }

    public string? UtmMedium { get; set; }

    public string? UtmCampaign { get; set; }

    public string? UtmTerm { get; set; }

    public string? UtmContent { get; set; }

    public string? Ref { get; set; }

    public virtual User? User { get; set; }

    public string? UserId { get; set; }

    public virtual Project? Project { get; set; }

    public string? ProjectId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
