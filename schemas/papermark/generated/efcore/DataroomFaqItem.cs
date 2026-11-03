#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class DataroomFaqItem
{
    public string Id { get; set; } = null!;

    public string? Title { get; set; }

    public string EditedQuestion { get; set; } = null!;

    public string? OriginalQuestion { get; set; }

    public string Answer { get; set; } = null!;

    public string? Description { get; set; }

    public string DataroomId { get; set; } = null!;

    public virtual Dataroom Dataroom { get; set; } = null!;

    public string? LinkId { get; set; }

    public virtual Link? Link { get; set; }

    public string? DataroomDocumentId { get; set; }

    public virtual DataroomDocument? DataroomDocument { get; set; }

    public string? SourceConversationId { get; set; }

    public virtual Conversation? SourceConversation { get; set; }

    public string? QuestionMessageId { get; set; }

    public virtual Message? QuestionMessage { get; set; }

    public string? AnswerMessageId { get; set; }

    public virtual Message? AnswerMessage { get; set; }

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public string PublishedByUserId { get; set; } = null!;

    public virtual User PublishedByUser { get; set; } = null!;

    public FaqVisibility VisibilityMode { get; set; } = FaqVisibility.PublicDataroom;

    public FaqStatus Status { get; set; } = FaqStatus.Published;

    public bool IsAnonymized { get; set; } = true;

    public int ViewCount { get; set; } = 0;

    public List<string>? Tags { get; set; } = new List<string>();

    public int? DocumentPageNumber { get; set; }

    public int? DocumentVersionNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
