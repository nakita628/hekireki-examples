#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class View
{
    public string Id { get; set; } = null!;

    public virtual Link Link { get; set; } = null!;

    public string LinkId { get; set; } = null!;

    public virtual Document? Document { get; set; }

    public string? DocumentId { get; set; }

    public virtual Dataroom? Dataroom { get; set; }

    public string? DataroomId { get; set; }

    public string? DataroomViewId { get; set; }

    public string? ViewerEmail { get; set; }

    public string? ViewerName { get; set; }

    public bool Verified { get; set; } = false;

    public DateTime ViewedAt { get; set; }

    public DateTime? DownloadedAt { get; set; }

    public DownloadType? DownloadType { get; set; }

    public string? DownloadMetadata { get; set; }

    public virtual ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();

    public ViewType ViewType { get; set; } = ViewType.DocumentView;

    public string? ViewerId { get; set; }

    public virtual Viewer? Viewer { get; set; }

    public string? GroupId { get; set; }

    public virtual ViewerGroup? Group { get; set; }

    public virtual FeedbackResponse? FeedbackResponse { get; set; }

    public virtual AgreementResponse? AgreementResponse { get; set; }

    public virtual CustomFieldResponse? CustomFieldResponse { get; set; }

    public bool IsArchived { get; set; } = false;

    public virtual ICollection<ConversationView> ConversationViews { get; set; } = new List<ConversationView>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Conversation> InitialConversations { get; set; } = new List<Conversation>();

    public virtual ICollection<DocumentUpload> UploadedDocuments { get; set; } = new List<DocumentUpload>();

    public virtual ICollection<TaskActivity> TaskActivities { get; set; } = new List<TaskActivity>();

    public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();

    public string? TeamId { get; set; }

    public virtual Team? Team { get; set; }
}
