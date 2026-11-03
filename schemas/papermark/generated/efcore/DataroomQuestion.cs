#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class DataroomQuestion
{
    public string Id { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? Category { get; set; }

    public string? Priority { get; set; }

    public string Status { get; set; } = "unanswered";

    public int? OrderIndex { get; set; }

    public string? ImportBatchId { get; set; }

    public string DataroomId { get; set; } = null!;

    public virtual Dataroom Dataroom { get; set; } = null!;

    public string TeamId { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;

    public string? CreatedByUserId { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public string? DataroomDocumentId { get; set; }

    public virtual DataroomDocument? DataroomDocument { get; set; }

    public int? DocumentPageNumber { get; set; }

    public virtual ICollection<DataroomQuestionAssignment> Assignments { get; set; } = new List<DataroomQuestionAssignment>();

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
