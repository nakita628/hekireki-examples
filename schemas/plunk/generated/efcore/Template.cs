#nullable enable

using System;
using System.Collections.Generic;

namespace Plunk.Models;

public partial class Template
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string From { get; set; } = null!;

    public string? FromName { get; set; }

    public string? ReplyTo { get; set; }

    public TemplateType Type { get; set; } = TemplateType.Marketing;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    public virtual ICollection<WorkflowStep> WorkflowSteps { get; set; } = new List<WorkflowStep>();

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
