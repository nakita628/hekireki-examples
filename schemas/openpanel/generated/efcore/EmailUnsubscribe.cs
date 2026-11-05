#nullable enable

using System;

namespace OpenPanel.Models;

public partial class EmailUnsubscribe
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Category { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
