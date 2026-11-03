#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class SurveyQuota
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Survey Survey { get; set; } = null!;

    public string SurveyId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Limit { get; set; }

    public string Logic { get; set; } = "{}";

    public SurveyQuotaAction Action { get; set; }

    public string? EndingCardId { get; set; }

    public virtual ICollection<ResponseQuotaLink> QuotaLinks { get; set; } = new List<ResponseQuotaLink>();

    public bool CountPartialSubmissions { get; set; } = false;
}
