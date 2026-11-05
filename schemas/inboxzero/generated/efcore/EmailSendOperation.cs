#nullable enable

using System;

namespace InboxZero.Models;

public partial class EmailSendOperation
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string ClientMutationId { get; set; } = null!;

    public string PayloadHash { get; set; } = null!;

    public EmailSendOperationStatus Status { get; set; } = EmailSendOperationStatus.Processing;

    public DateTime ProcessingStartedAt { get; set; }

    public string? Result { get; set; }

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;
}
