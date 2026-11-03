#nullable enable

using System;

namespace Documenso.Models;

public partial class CscSession
{
    public string Id { get; set; } = null!;

    public string EnvelopeId { get; set; } = null!;

    public DateTime SigningTime { get; set; }

    public string ItemsJson { get; set; } = null!;

    public byte[]? EncryptedSad { get; set; }

    public DateTime? SadExpiresAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public int RecipientId { get; set; }

    public virtual Recipient Recipient { get; set; } = null!;
}
