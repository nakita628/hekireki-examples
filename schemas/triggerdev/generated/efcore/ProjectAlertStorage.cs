#nullable enable

using System;

namespace Trigger.Models;

public partial class ProjectAlertStorage
{
    public string Id { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual ProjectAlertChannel AlertChannel { get; set; } = null!;

    public string AlertChannelId { get; set; } = null!;

    public ProjectAlertType AlertType { get; set; }

    public string StorageId { get; set; } = null!;

    public string StorageData { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
