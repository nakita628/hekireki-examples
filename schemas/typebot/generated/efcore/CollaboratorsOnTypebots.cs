#nullable enable

using System;

namespace Typebot.Models;

public partial class CollaboratorsOnTypebots
{
    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string UserId { get; set; } = null!;

    public string TypebotId { get; set; } = null!;

    public CollaborationType Type { get; set; }

    public virtual Typebot Typebot { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
