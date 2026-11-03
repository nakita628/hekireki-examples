#nullable enable

using System;
using System.Collections.Generic;

namespace Documenso.Models;

public partial class Recipient
{
    public int Id { get; set; }

    public string EnvelopeId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = "";

    public string Token { get; set; } = null!;

    public DateTime? DocumentDeletedAt { get; set; }

    public DateTime? Expired { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public DateTime? ExpirationNotifiedAt { get; set; }

    public DateTime? SentAt { get; set; }

    public DateTime? SignedAt { get; set; }

    public DateTime? LastReminderSentAt { get; set; }

    public DateTime? NextReminderAt { get; set; }

    public int ReminderCount { get; set; } = 0;

    public string? AuthOptions { get; set; }

    public int? SigningOrder { get; set; }

    public string? RejectionReason { get; set; }

    public RecipientRole Role { get; set; } = RecipientRole.Signer;

    public ReadStatus ReadStatus { get; set; } = ReadStatus.NotOpened;

    public SigningStatus SigningStatus { get; set; } = SigningStatus.NotSigned;

    public SendStatus SendStatus { get; set; } = SendStatus.NotSent;

    public virtual Envelope Envelope { get; set; } = null!;

    public virtual ICollection<Field> Fields { get; set; } = new List<Field>();

    public virtual ICollection<Signature> Signatures { get; set; } = new List<Signature>();

    public virtual CscCredential? CscCredential { get; set; }

    public virtual CscSession? CscSession { get; set; }
}
