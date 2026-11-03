#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class Agent
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual User? User { get; set; }

    public int? TeamId { get; set; }

    public virtual Team? Team { get; set; }

    public string ProviderAgentId { get; set; } = null!;

    public int? InboundEventTypeId { get; set; }

    public int? OutboundEventTypeId { get; set; }

    public bool Enabled { get; set; } = true;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<CalAiPhoneNumber> InboundPhoneNumbers { get; set; } = new List<CalAiPhoneNumber>();

    public virtual ICollection<CalAiPhoneNumber> OutboundPhoneNumbers { get; set; } = new List<CalAiPhoneNumber>();
}
