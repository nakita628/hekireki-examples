#nullable enable

using System;
using System.Collections.Generic;

namespace Postiz.Models;

public partial class Webhooks
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string Url { get; set; } = null!;

    public DateTime? DeletedAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<IntegrationsWebhooks> Integrations { get; set; } = new List<IntegrationsWebhooks>();

    public virtual Organization Organization { get; set; } = null!;
}
