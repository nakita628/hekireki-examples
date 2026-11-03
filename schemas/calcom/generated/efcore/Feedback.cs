#nullable enable

using System;

namespace Calcom.Models;

public partial class Feedback
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public string Rating { get; set; } = null!;

    public string? Comment { get; set; }
}
