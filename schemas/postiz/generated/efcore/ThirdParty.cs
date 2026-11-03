#nullable enable

using System;

namespace Postiz.Models;

public partial class ThirdParty
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Identifier { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string InternalId { get; set; } = null!;

    public string ApiKey { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Organization Organization { get; set; } = null!;
}
