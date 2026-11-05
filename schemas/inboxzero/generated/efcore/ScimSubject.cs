#nullable enable

using System;

namespace InboxZero.Models;

public partial class ScimSubject
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public string? ProfileSourceId { get; set; }

    public int Revision { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
