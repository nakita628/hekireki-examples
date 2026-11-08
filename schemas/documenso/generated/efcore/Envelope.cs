#nullable enable

using System;
using System.Collections.Generic;

namespace Documenso.Models;

public partial class Envelope
{
    public string Id { get; set; } = null!;

    public string SecondaryId { get; set; } = null!;

    public string? ExternalId { get; set; }

    public EnvelopeType Type { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string Title { get; set; } = null!;

    public DocumentStatus Status { get; set; } = DocumentStatus.Draft;

    public DocumentSource Source { get; set; }

    public string? QrToken { get; set; }

    public string SignatureLevel { get; set; } = null!;

    public int InternalVersion { get; set; }

    public bool UseLegacyFieldInsertion { get; set; } = false;

    public virtual ICollection<EnvelopeItem> EnvelopeItems { get; set; } = new List<EnvelopeItem>();

    public virtual ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();

    public virtual ICollection<Field> Fields { get; set; } = new List<Field>();

    public virtual ICollection<EnvelopeContent> Contents { get; set; } = new List<EnvelopeContent>();

    public virtual ICollection<DocumentShareLink> ShareLinks { get; set; } = new List<DocumentShareLink>();

    public virtual ICollection<DocumentAuditLog> AuditLogs { get; set; } = new List<DocumentAuditLog>();

    public string? AuthOptions { get; set; }

    public string? FormValues { get; set; }

    public DocumentVisibility Visibility { get; set; } = DocumentVisibility.Everyone;

    public TemplateType TemplateType { get; set; } = TemplateType.Private;

    public string PublicTitle { get; set; } = "";

    public string PublicDescription { get; set; } = "";

    public virtual TemplateDirectLink? DirectLink { get; set; }

    public int? TemplateId { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public int TeamId { get; set; }

    public virtual Team Team { get; set; } = null!;

    public virtual Folder? Folder { get; set; }

    public string? FolderId { get; set; }

    public string DocumentMetaId { get; set; } = null!;

    public virtual DocumentMeta DocumentMeta { get; set; } = null!;

    public virtual ICollection<EnvelopeAttachment> EnvelopeAttachments { get; set; } = new List<EnvelopeAttachment>();
}
