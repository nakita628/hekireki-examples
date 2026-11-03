#nullable enable

using System;

namespace Calcom.Models;

public partial class BookingInternalNote
{
    public int Id { get; set; }

    public virtual InternalNotePreset? NotePreset { get; set; }

    public int? NotePresetId { get; set; }

    public string? Text { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public int BookingId { get; set; }

    public virtual User CreatedBy { get; set; } = null!;

    public int CreatedById { get; set; }

    public DateTime CreatedAt { get; set; }
}
