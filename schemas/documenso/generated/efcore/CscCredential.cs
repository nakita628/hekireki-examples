#nullable enable

using System;

namespace Documenso.Models;

public partial class CscCredential
{
    public string Id { get; set; } = null!;

    public string ProviderId { get; set; } = null!;

    public string CredentialId { get; set; } = null!;

    public byte[]? CertCache { get; set; }

    public string SignatureAlgorithm { get; set; } = null!;

    public string KeyType { get; set; } = null!;

    public string DigestAlgorithm { get; set; } = null!;

    public int? KeyLenBits { get; set; }

    public string? SignAlgoParams { get; set; }

    public byte[]? ServiceTokenCiphertext { get; set; }

    public DateTime? ServiceTokenExpiresAt { get; set; }

    public int RecipientId { get; set; }

    public virtual Recipient Recipient { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
