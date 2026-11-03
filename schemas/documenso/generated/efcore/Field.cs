#nullable enable

namespace Documenso.Models;

public partial class Field
{
    public int Id { get; set; }

    public string SecondaryId { get; set; } = null!;

    public string EnvelopeId { get; set; } = null!;

    public string EnvelopeItemId { get; set; } = null!;

    public int RecipientId { get; set; }

    public FieldType Type { get; set; }

    public int Page { get; set; }

    public decimal PositionX { get; set; } = 0m;

    public decimal PositionY { get; set; } = 0m;

    public decimal Width { get; set; } = -1m;

    public decimal Height { get; set; } = -1m;

    public string CustomText { get; set; } = null!;

    public bool Inserted { get; set; }

    public virtual EnvelopeItem EnvelopeItem { get; set; } = null!;

    public virtual Envelope Envelope { get; set; } = null!;

    public virtual Recipient Recipient { get; set; } = null!;

    public virtual Signature? Signature { get; set; }

    public string? FieldMeta { get; set; }
}
