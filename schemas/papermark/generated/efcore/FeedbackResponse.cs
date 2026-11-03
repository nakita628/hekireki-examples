#nullable enable

using System;

namespace Papermark.Models;

public partial class FeedbackResponse
{
    public string Id { get; set; } = null!;

    public string FeedbackId { get; set; } = null!;

    public virtual Feedback Feedback { get; set; } = null!;

    public string Data { get; set; } = null!;

    public string ViewId { get; set; } = null!;

    public virtual View View { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
