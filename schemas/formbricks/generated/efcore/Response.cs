#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class Response
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public bool Finished { get; set; } = false;

    public virtual Survey Survey { get; set; } = null!;

    public string SurveyId { get; set; } = null!;

    public virtual Contact? Contact { get; set; }

    public string? ContactId { get; set; }

    public string? EndingId { get; set; }

    public string Data { get; set; } = "{}";

    public string Variables { get; set; } = "{}";

    public string Ttc { get; set; } = "{}";

    public string Meta { get; set; } = "{}";

    public string? IngestFlags { get; set; }

    public ResponseOutcome? Outcome { get; set; }

    public string? NotShown { get; set; }

    public virtual ICollection<TagsOnResponses> Tags { get; set; } = new List<TagsOnResponses>();

    public virtual ICollection<ResponseQuotaLink> QuotaLinks { get; set; } = new List<ResponseQuotaLink>();

    public string? ContactAttributes { get; set; }

    public string? SingleUseId { get; set; }

    public string? Language { get; set; }

    public string? DisplayId { get; set; }

    public virtual Display? Display { get; set; }

    public virtual ICollection<WorkflowRun> WorkflowRuns { get; set; } = new List<WorkflowRun>();
}
