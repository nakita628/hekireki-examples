#nullable enable

using System;

namespace Papermark.Models;

public partial class AgreementResponse
{
    public string Id { get; set; } = null!;

    public string AgreementId { get; set; } = null!;

    public virtual Agreement Agreement { get; set; } = null!;

    public string? ViewId { get; set; }

    public virtual View? View { get; set; }

    public string? LinkId { get; set; }

    public string? SignerEmail { get; set; }

    public string? SignerName { get; set; }

    public string SigningStatus { get; set; } = "PENDING";

    public string? SigningExternalId { get; set; }

    public string? SigningEnvelopeId { get; set; }

    public int? SigningDocumentId { get; set; }

    public DateTime? SignedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public string? SignedFileKey { get; set; }

    public string? SignedFileName { get; set; }

    public DocumentStorageType? SignedFileStorageType { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
