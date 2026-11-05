#nullable enable

using System;

namespace OpenPanel.Models;

public partial class ExportWatermark
{
    public Guid Id { get; set; }

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual Integration Integration { get; set; } = null!;

    public Guid IntegrationId { get; set; }

    public DateTime LastInsertedAt { get; set; }

    public string LastEventId { get; set; } = "";

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
