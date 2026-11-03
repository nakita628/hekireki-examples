#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Agreement
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string ContentType { get; set; } = "LINK";

    public string SigningProvider { get; set; } = "LEGACY";

    public string? SigningExternalId { get; set; }

    public string? SigningEnvelopeId { get; set; }

    public string? SigningTemplateId { get; set; }

    public virtual ICollection<Link> Links { get; set; } = new List<Link>();

    public virtual ICollection<AgreementResponse> Responses { get; set; } = new List<AgreementResponse>();

    public bool RequireName { get; set; } = true;

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}
