#nullable enable

using System;
using System.Collections.Generic;

namespace Typebot.Models;

public partial class Result
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string TypebotId { get; set; } = null!;

    public string Variables { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public bool? HasStarted { get; set; }

    public bool? IsArchived { get; set; } = false;

    public string? LastChatSessionId { get; set; }

    public virtual Typebot Typebot { get; set; } = null!;

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<AnswerV2> AnswersV2 { get; set; } = new List<AnswerV2>();

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual ICollection<VisitedEdge> Edges { get; set; } = new List<VisitedEdge>();

    public virtual ICollection<SetVariableHistoryItem> SetVariableHistory { get; set; } = new List<SetVariableHistoryItem>();
}
