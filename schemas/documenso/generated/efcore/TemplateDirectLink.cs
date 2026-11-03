#nullable enable

using System;

namespace Documenso.Models;

public partial class TemplateDirectLink
{
    public string Id { get; set; } = null!;

    public string EnvelopeId { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool Enabled { get; set; }

    public int DirectTemplateRecipientId { get; set; }

    public virtual Envelope Envelope { get; set; } = null!;
}
