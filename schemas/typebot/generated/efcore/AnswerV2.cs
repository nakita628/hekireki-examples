#nullable enable

using System;

namespace Typebot.Models;

public partial class AnswerV2
{
    public int Id { get; set; }

    public DateTime CreatedAt { get; set; }

    public string BlockId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? AttachedFileUrls { get; set; }

    public string ResultId { get; set; } = null!;

    public virtual Result Result { get; set; } = null!;
}
