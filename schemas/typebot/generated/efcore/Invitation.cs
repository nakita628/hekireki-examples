#nullable enable

using System;

namespace Typebot.Models;

public partial class Invitation
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Email { get; set; } = null!;

    public string TypebotId { get; set; } = null!;

    public CollaborationType Type { get; set; }

    public virtual Typebot Typebot { get; set; } = null!;
}
