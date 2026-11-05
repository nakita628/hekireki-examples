#nullable enable

using System;
using System.Collections.Generic;

namespace OpenPanel.Models;

public partial class Project
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual Organization Organization { get; set; } = null!;

    public string OrganizationId { get; set; } = null!;

    public int EventsCount { get; set; } = 0;

    public List<ProjectType>? Types { get; set; } = new List<ProjectType>();

    public string? Domain { get; set; }

    public List<string>? Cors { get; set; } = new List<string>();

    public bool CrossDomain { get; set; } = false;

    public bool AllowUnsafeRevenueTracking { get; set; } = false;

    public bool AiStoreContent { get; set; } = true;

    public string Filters { get; set; } = "[]";

    public DateTime? FirstEventAt { get; set; }

    public DateTime? NoDataNotifiedAt { get; set; }

    public DateTime? DataStoppedNotifiedAt { get; set; }

    public virtual ICollection<Client> Clients { get; set; } = new List<Client>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual ICollection<Dashboard> Dashboards { get; set; } = new List<Dashboard>();

    public virtual ShareOverview? Share { get; set; }

    public virtual ICollection<ShareDashboard> ShareDashboards { get; set; } = new List<ShareDashboard>();

    public virtual ICollection<ShareReport> ShareReports { get; set; } = new List<ShareReport>();

    public virtual ICollection<ShareWidget> ShareWidgets { get; set; } = new List<ShareWidget>();

    public virtual ICollection<EventMeta> Meta { get; set; } = new List<EventMeta>();

    public virtual ICollection<CustomEvent> CustomEvents { get; set; } = new List<CustomEvent>();

    public virtual ICollection<Reference> References { get; set; } = new List<Reference>();

    public virtual ICollection<MemberProject> MemberProjects { get; set; } = new List<MemberProject>();

    public virtual ICollection<NotificationRule> NotificationRules { get; set; } = new List<NotificationRule>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Import> Imports { get; set; } = new List<Import>();

    public virtual GscConnection? GscConnection { get; set; }

    public virtual ICollection<Cohort> Cohorts { get; set; } = new List<Cohort>();

    public virtual ICollection<ExportWatermark> ExportWatermarks { get; set; } = new List<ExportWatermark>();

    public virtual ICollection<Integration> Integrations { get; set; } = new List<Integration>();

    public DateTime? DeleteAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();
}
