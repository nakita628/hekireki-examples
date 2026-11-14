#nullable enable

using System;

namespace LangWatch.Models;

public partial class SigningKey
{
    public string Id { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string PublicId { get; set; } = null!;

    public string SecretCiphertext { get; set; } = null!;

    public string CreatedByUserId { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? RotatedAt { get; set; }

    public DateTime? VerifiesUntil { get; set; }

    public DateTime? RevokedAt { get; set; }

    public DateTime? LastUsedAt { get; set; }
}
