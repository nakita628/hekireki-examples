#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Conversation
{
    public string Id { get; set; } = null!;

    public string? Title { get; set; }

    public bool IsEnabled { get; set; } = true;

    public ConversationVisibility VisibilityMode { get; set; } = ConversationVisibility.Private;

    public string DataroomId { get; set; } = null!;

    public virtual Dataroom Dataroom { get; set; } = null!;

    public string? DataroomDocumentId { get; set; }

    public virtual DataroomDocument? DataroomDocument { get; set; }

    public int? DocumentVersionNumber { get; set; }

    public int? DocumentPageNumber { get; set; }

    public string? LinkId { get; set; }

    public virtual Link? Link { get; set; }

    public string? ViewerGroupId { get; set; }

    public virtual ViewerGroup? ViewerGroup { get; set; }

    public string? QuestionId { get; set; }

    public virtual DataroomQuestion? Question { get; set; }

    public string Origin { get; set; } = "visitor";

    public string? Category { get; set; }

    public string? Priority { get; set; }

    public string? RecipientViewerId { get; set; }

    public virtual Viewer? RecipientViewer { get; set; }

    public string? InitialViewId { get; set; }

    public virtual View? InitialView { get; set; }

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<ConversationView> Views { get; set; } = new List<ConversationView>();

    public virtual ICollection<ConversationParticipant> Participants { get; set; } = new List<ConversationParticipant>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public DateTime? LastMessageAt { get; set; }

    public virtual ICollection<DataroomFaqItem> FaqItems { get; set; } = new List<DataroomFaqItem>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
