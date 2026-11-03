#nullable enable

using System;

namespace Calcom.Models;

public partial class OutOfOfficeEntry
{
    public int Id { get; set; }

    public string Uuid { get; set; } = null!;

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public string? Notes { get; set; }

    public bool ShowNotePublicly { get; set; } = false;

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public int? ToUserId { get; set; }

    public virtual User? ToUser { get; set; }

    public int? ReasonId { get; set; }

    public virtual OutOfOfficeReason? Reason { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
