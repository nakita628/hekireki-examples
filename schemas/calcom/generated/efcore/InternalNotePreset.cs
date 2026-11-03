#nullable enable

using System;
using System.Collections.Generic;

namespace Calcom.Models;

public partial class InternalNotePreset
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? CancellationReason { get; set; }

    public virtual Team Team { get; set; } = null!;

    public int TeamId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<BookingInternalNote> BookingInternalNote { get; set; } = new List<BookingInternalNote>();
}
