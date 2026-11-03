#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class Profile
{
    public int Id { get; set; }

    public string Uid { get; set; } = null!;

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public int OrganizationId { get; set; }

    public virtual Team Organization { get; set; } = null!;

    public string Username { get; set; } = null!;

    public virtual ICollection<EventType> EventTypes { get; set; } = new List<EventType>();

    public virtual User? MovedFromUser { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
