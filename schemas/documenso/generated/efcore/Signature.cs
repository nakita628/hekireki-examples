#nullable enable

using System;

namespace Documenso.Models;

public partial class Signature
{
    public int Id { get; set; }

    public DateTime Created { get; set; }

    public int RecipientId { get; set; }

    public int FieldId { get; set; }

    public string? SignatureImageAsBase64 { get; set; }

    public string? TypedSignature { get; set; }

    public virtual Recipient Recipient { get; set; } = null!;

    public virtual Field Field { get; set; } = null!;
}
