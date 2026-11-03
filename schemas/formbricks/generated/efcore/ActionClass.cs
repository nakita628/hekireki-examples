#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class ActionClass
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public ActionType Type { get; set; }

    public string? Key { get; set; }

    public string? NoCodeConfig { get; set; }

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual ICollection<SurveyTrigger> SurveyTriggers { get; set; } = new List<SurveyTrigger>();

    public virtual ICollection<CreativeTrigger> CreativeTriggers { get; set; } = new List<CreativeTrigger>();
}
