#nullable enable

using System;

namespace OpenSaas.Models;

public partial class GptResponse
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Content { get; set; } = null!;
}
