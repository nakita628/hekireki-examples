#nullable enable

using System;
using System.Collections.Generic;

namespace Dub.Models;

public partial class Link
{
    public string Id { get; set; } = null!;

    public string Domain { get; set; } = null!;

    public string Key { get; set; } = null!;

    public string Url { get; set; } = null!;

    public string ShortLink { get; set; } = null!;

    public bool Archived { get; set; } = false;

    public DateTime? ExpiresAt { get; set; }

    public string? ExpiredUrl { get; set; }

    public DateTime? DisabledAt { get; set; }

    public string? Password { get; set; }

    public bool TrackConversion { get; set; } = false;

    public bool Proxy { get; set; } = false;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public string? Video { get; set; }

    public string? UtmSource { get; set; }

    public string? UtmMedium { get; set; }

    public string? UtmCampaign { get; set; }

    public string? UtmTerm { get; set; }

    public string? UtmContent { get; set; }

    public bool Rewrite { get; set; } = false;

    public DateTime? LinkRetentionCleanupDisabledAt { get; set; }

    public bool DoIndex { get; set; } = false;

    public string? Ios { get; set; }

    public string? Android { get; set; }

    public string? Geo { get; set; }

    public string? TestVariants { get; set; }

    public DateTime? TestStartedAt { get; set; }

    public DateTime? TestCompletedAt { get; set; }

    public virtual User? User { get; set; }

    public string? UserId { get; set; }

    public virtual Project? Project { get; set; }

    public string? ProjectId { get; set; }

    public string? FolderId { get; set; }

    public virtual Folder? Folder { get; set; }

    public string? ExternalId { get; set; }

    public string? TenantId { get; set; }

    public bool PublicStats { get; set; } = false;

    public int Clicks { get; set; } = 0;

    public int Leads { get; set; } = 0;

    public int Conversions { get; set; } = 0;

    public int Sales { get; set; } = 0;

    public long SaleAmount { get; set; } = 0L;

    public DateTime? LastClicked { get; set; }

    public DateTime? LastLeadAt { get; set; }

    public DateTime? LastConversionAt { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Domain ShortDomain { get; set; } = null!;

    public virtual ICollection<LinkTag> Tags { get; set; } = new List<LinkTag>();

    public virtual ICollection<LinkWebhook> Webhooks { get; set; } = new List<LinkWebhook>();

    public string? Comments { get; set; }

    public virtual Dashboard? Dashboard { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
}
