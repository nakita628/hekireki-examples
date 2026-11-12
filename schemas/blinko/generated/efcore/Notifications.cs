#nullable enable

using System;

namespace Blinko.Models;

public partial class Notifications
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public string? Metadata { get; set; }

    public bool IsRead { get; set; } = false;

    public int AccountId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Accounts Account { get; set; } = null!;
}
