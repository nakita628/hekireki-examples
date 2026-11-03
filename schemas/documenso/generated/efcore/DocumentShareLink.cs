#nullable enable

using System;

namespace Documenso.Models;

public partial class DocumentShareLink
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string EnvelopeId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Envelope Envelope { get; set; } = null!;
}
