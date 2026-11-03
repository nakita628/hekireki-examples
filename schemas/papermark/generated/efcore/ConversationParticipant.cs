#nullable enable

using System;

namespace Papermark.Models;

public partial class ConversationParticipant
{
    public string Id { get; set; } = null!;

    public string ConversationId { get; set; } = null!;

    public virtual Conversation Conversation { get; set; } = null!;

    public ParticipantRole Role { get; set; } = ParticipantRole.Participant;

    public string? ViewerId { get; set; }

    public virtual Viewer? Viewer { get; set; }

    public string? UserId { get; set; }

    public virtual User? User { get; set; }

    public bool ReceiveNotifications { get; set; } = false;

    public DateTime CreatedAt { get; set; }
}
