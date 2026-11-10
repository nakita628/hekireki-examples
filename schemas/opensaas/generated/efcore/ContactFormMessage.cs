#nullable enable

using System;

namespace OpenSaas.Models;

public partial class ContactFormMessage
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public bool IsRead { get; set; } = false;

    public DateTime? RepliedAt { get; set; }
}
