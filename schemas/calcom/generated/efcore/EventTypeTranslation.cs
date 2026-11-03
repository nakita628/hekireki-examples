#nullable enable

using System;

namespace Calcom.Models;

public partial class EventTypeTranslation
{
    public string Uid { get; set; } = null!;

    public virtual EventType EventType { get; set; } = null!;

    public int EventTypeId { get; set; }

    public EventTypeAutoTranslatedField Field { get; set; }

    public string SourceLocale { get; set; } = null!;

    public string TargetLocale { get; set; } = null!;

    public string TranslatedText { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual User Creator { get; set; } = null!;

    public virtual User? Updater { get; set; }
}
