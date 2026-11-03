#nullable enable

using System;

namespace Typebot.Models;

public partial class Answer
{
    public DateTime CreatedAt { get; set; }

    public string ResultId { get; set; } = null!;

    public string BlockId { get; set; } = null!;

    public string GroupId { get; set; } = null!;

    public string? VariableId { get; set; }

    public string Content { get; set; } = null!;

    public virtual Result Result { get; set; } = null!;
}
