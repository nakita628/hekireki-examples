#nullable enable

using System;
using System.Collections.Generic;

namespace Umami.Models;

public partial class Website
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Domain { get; set; }

    public DateTime? ResetAt { get; set; }

    public Guid? UserId { get; set; }

    public Guid? TeamId { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public bool RecorderEnabled { get; set; } = false;

    public string? ReplayConfig { get; set; }

    public virtual User? User { get; set; }

    public virtual User? CreateUser { get; set; }

    public virtual Team? Team { get; set; }

    public virtual ICollection<Annotation> Annotations { get; set; } = new List<Annotation>();

    public virtual ICollection<EventData> EventData { get; set; } = new List<EventData>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Revenue> Revenue { get; set; } = new List<Revenue>();

    public virtual ICollection<Segment> Segments { get; set; } = new List<Segment>();

    public virtual ICollection<SessionData> SessionData { get; set; } = new List<SessionData>();

    public virtual ICollection<SessionReplay> SessionReplays { get; set; } = new List<SessionReplay>();

    public virtual ICollection<SessionReplaySaved> SessionReplaysSaved { get; set; } = new List<SessionReplaySaved>();

    public virtual ICollection<HeatmapEvent> HeatmapEvents { get; set; } = new List<HeatmapEvent>();
}
