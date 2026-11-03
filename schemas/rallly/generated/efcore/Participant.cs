#nullable enable

using System;
using System.Collections.Generic;

namespace Rallly.Models;

public partial class Participant
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Note { get; set; }

    public string Token { get; set; } = null!;

    public string? UserId { get; set; }

    public string PollId { get; set; } = null!;

    public string? Locale { get; set; }

    public string? TimeZone { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();

    public virtual PollInvite? Invite { get; set; }

    public virtual Poll Poll { get; set; } = null!;

    public virtual User? User { get; set; }
}
