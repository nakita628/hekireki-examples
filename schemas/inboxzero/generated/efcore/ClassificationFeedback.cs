#nullable enable

using System;

namespace InboxZero.Models;

public partial class ClassificationFeedback
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Sender { get; set; } = null!;

    public ClassificationFeedbackEventType EventType { get; set; }

    public string RuleId { get; set; } = null!;

    public virtual Rule Rule { get; set; } = null!;

    public string EmailAccountId { get; set; } = null!;

    public virtual EmailAccount EmailAccount { get; set; } = null!;

    public string ThreadId { get; set; } = null!;

    public string MessageId { get; set; } = null!;
}
