#nullable enable

using System;
using System.Collections.Generic;

namespace Umami.Models;

public partial class WebsiteEvent
{
    public Guid Id { get; set; }

    public Guid WebsiteId { get; set; }

    public Guid SessionId { get; set; }

    public Guid VisitId { get; set; }

    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    public string UrlPath { get; set; } = null!;

    public string? UrlQuery { get; set; }

    public string? UtmSource { get; set; }

    public string? UtmMedium { get; set; }

    public string? UtmCampaign { get; set; }

    public string? UtmContent { get; set; }

    public string? UtmTerm { get; set; }

    public string? ReferrerPath { get; set; }

    public string? ReferrerQuery { get; set; }

    public string? ReferrerDomain { get; set; }

    public string? PageTitle { get; set; }

    public string? Gclid { get; set; }

    public string? Fbclid { get; set; }

    public string? Msclkid { get; set; }

    public string? Ttclid { get; set; }

    public string? Lifatid { get; set; }

    public string? Twclid { get; set; }

    public int EventType { get; set; } = 1;

    public string? EventName { get; set; }

    public string? Tag { get; set; }

    public string? Hostname { get; set; }

    public decimal? Lcp { get; set; }

    public decimal? Inp { get; set; }

    public decimal? Cls { get; set; }

    public decimal? Fcp { get; set; }

    public decimal? Ttfb { get; set; }

    public virtual ICollection<EventData> EventData { get; set; } = new List<EventData>();

    public virtual Session Session { get; set; } = null!;
}
