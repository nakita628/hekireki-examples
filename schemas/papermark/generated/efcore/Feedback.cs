#nullable enable

using System;
using System.Collections.Generic;

namespace Papermark.Models;

public partial class Feedback
{
    public string Id { get; set; } = null!;

    public string LinkId { get; set; } = null!;

    public virtual Link Link { get; set; } = null!;

    public string Data { get; set; } = null!;

    public virtual ICollection<FeedbackResponse> Responses { get; set; } = new List<FeedbackResponse>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
