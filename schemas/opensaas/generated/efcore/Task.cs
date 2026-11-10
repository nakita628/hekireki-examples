#nullable enable

using System;

namespace OpenSaas.Models;

public partial class Task
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Time { get; set; } = "1";

    public bool IsDone { get; set; } = false;
}
