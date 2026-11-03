#nullable enable

using System;
using System.Collections.Generic;

namespace Formbricks.Models;

public partial class EmbeddedData
{
    public string Id { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Key { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public EmbeddedDataSource Source { get; set; }

    public EmbeddedDataType DataType { get; set; } = EmbeddedDataType.String;

    public string? DefaultValue { get; set; }

    public bool Locked { get; set; } = false;

    public virtual Survey? Survey { get; set; }

    public string? SurveyId { get; set; }

    public virtual Workspace Workspace { get; set; } = null!;

    public string WorkspaceId { get; set; } = null!;

    public virtual ICollection<SurveyEmbeddedData> SurveyLinks { get; set; } = new List<SurveyEmbeddedData>();
}
