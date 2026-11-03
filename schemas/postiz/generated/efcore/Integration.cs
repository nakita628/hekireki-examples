#nullable enable

using System;
using System.Collections.Generic;

namespace Postiz.Models;

public partial class Integration
{
    public string Id { get; set; } = null!;

    public string InternalId { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? CustomName { get; set; }

    public string? Picture { get; set; }

    public string ProviderIdentifier { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Token { get; set; } = null!;

    public bool Disabled { get; set; } = false;

    public DateTime? TokenExpiration { get; set; }

    public string? RefreshToken { get; set; }

    public string? Profile { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool InBetweenSteps { get; set; } = false;

    public bool RefreshNeeded { get; set; } = false;

    public string PostingTimes { get; set; } = "[{\"time\":120}, {\"time\":400}, {\"time\":700}]";

    public string? CustomInstanceDetails { get; set; }

    public string? CustomerId { get; set; }

    public string? RootInternalId { get; set; }

    public string? AdditionalSettings { get; set; } = "[]";

    public virtual ICollection<ExisingPlugData> ExisingPlugData { get; set; } = new List<ExisingPlugData>();

    public virtual Customer? Customer { get; set; }

    public virtual Organization Organization { get; set; } = null!;

    public virtual ICollection<IntegrationsWebhooks> Webhooks { get; set; } = new List<IntegrationsWebhooks>();

    public virtual ICollection<OrderItems> OrderItems { get; set; } = new List<OrderItems>();

    public virtual ICollection<Plugs> Plugs { get; set; } = new List<Plugs>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
