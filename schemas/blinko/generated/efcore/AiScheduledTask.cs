#nullable enable

using System;

namespace Blinko.Models;

public partial class AiScheduledTask
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Prompt { get; set; } = null!;

    public string Schedule { get; set; } = null!;

    public bool IsEnabled { get; set; } = true;

    public DateTime? LastRun { get; set; }

    public string? LastResult { get; set; }

    public int AccountId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Accounts Account { get; set; } = null!;
}
