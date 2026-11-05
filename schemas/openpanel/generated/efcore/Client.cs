#nullable enable

using System;

namespace OpenPanel.Models;

public partial class Client
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Secret { get; set; }

    public ClientType Type { get; set; } = ClientType.Write;

    public string? ProjectId { get; set; }

    public virtual Project? Project { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public bool IgnoreCorsAndSecret { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
