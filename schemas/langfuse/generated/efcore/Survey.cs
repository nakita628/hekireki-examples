#nullable enable

using System;

namespace Langfuse.Models;

public partial class Survey
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public SurveyName SurveyName { get; set; }

    public string Response { get; set; } = null!;

    public string? UserId { get; set; }

    public string? UserEmail { get; set; }

    public string? OrgId { get; set; }

    public virtual Organization? Org { get; set; }

    public virtual User? User { get; set; }
}
