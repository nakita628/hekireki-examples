#nullable enable

using System;

namespace Documenso.Models;

public partial class EnvelopeAttachment
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Label { get; set; } = null!;

    public string Data { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string EnvelopeId { get; set; } = null!;

    public virtual Envelope Envelope { get; set; } = null!;
}
