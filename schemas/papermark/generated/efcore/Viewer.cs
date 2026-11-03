#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Viewer
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool Verified { get; set; } = false;

    public DateTime? InvitedAt { get; set; }

    public string? NotificationPreferences { get; set; }

    public string? DataroomId { get; set; }

    public virtual Dataroom? Dataroom { get; set; }

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<View> Views { get; set; } = new List<View>();

    public virtual ICollection<ViewerGroupMembership> Groups { get; set; } = new List<ViewerGroupMembership>();

    public virtual ICollection<ViewerInvitation> Invitations { get; set; } = new List<ViewerInvitation>();

    public virtual ICollection<ConversationParticipant> ParticipatedConversations { get; set; } = new List<ConversationParticipant>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Conversation> RecipientConversations { get; set; } = new List<Conversation>();

    public virtual ICollection<DataroomQuestionAssignment> QuestionAssignments { get; set; } = new List<DataroomQuestionAssignment>();

    public virtual ICollection<DocumentUpload> UploadedDocuments { get; set; } = new List<DocumentUpload>();

    public virtual ICollection<TaskAssignment> TaskAssignments { get; set; } = new List<TaskAssignment>();

    public virtual ICollection<TaskActivity> TaskActivities { get; set; } = new List<TaskActivity>();

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
