#nullable enable

namespace Documenso.Models;

public partial class EnvelopeContent
{
    public string Id { get; set; } = null!;

    public string EnvelopeId { get; set; } = null!;

    public string EnvelopeItemId { get; set; } = null!;

    public virtual EnvelopeItem EnvelopeItem { get; set; } = null!;

    public virtual Envelope Envelope { get; set; } = null!;

    public string ContentMeta { get; set; } = null!;

    public string? DataContentId { get; set; }

    public virtual DataContent? DataContent { get; set; }
}
