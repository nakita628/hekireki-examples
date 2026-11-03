#nullable enable

using System;

namespace Postiz.Models;

public partial class Credits
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public int Credits1 { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Type { get; set; } = "ai_images";

    public virtual Organization Organization { get; set; } = null!;
}
