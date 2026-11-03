#nullable enable

using System;

namespace Calcom.Models;

public partial class CalVideoSettings
{
    public int EventTypeId { get; set; }

    public virtual EventType EventType { get; set; } = null!;

    public bool DisableRecordingForOrganizer { get; set; } = false;

    public bool DisableRecordingForGuests { get; set; } = false;

    public bool EnableAutomaticTranscription { get; set; } = false;

    public bool EnableAutomaticRecordingForOrganizer { get; set; } = false;

    public string? RedirectUrlOnExit { get; set; }

    public bool DisableTranscriptionForGuests { get; set; } = false;

    public bool DisableTranscriptionForOrganizer { get; set; } = false;

    public bool RequireEmailForGuests { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
