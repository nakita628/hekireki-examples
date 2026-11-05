#nullable enable

using System;
using System.Collections.Generic;

namespace InboxZero.Models;

public partial class ContactResearch
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string Email { get; set; } = null!;

    public DateTime ResearchStartedAt { get; set; }

    public bool Found { get; set; }

    public string? Role { get; set; }

    public string? Confidence { get; set; }

    public string? Company { get; set; }

    public List<string>? Sources { get; set; } = new List<string>();
}
