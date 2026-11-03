#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class BackgroundWorkerFile
{
    public string Id { get; set; } = null!;

    public string FriendlyId { get; set; } = null!;

    public string FilePath { get; set; } = null!;

    public string ContentHash { get; set; } = null!;

    public byte[] Contents { get; set; } = null!;

    public virtual Project Project { get; set; } = null!;

    public string ProjectId { get; set; } = null!;

    public virtual ICollection<BackgroundWorker> BackgroundWorkers { get; set; } = new List<BackgroundWorker>();

    public virtual ICollection<BackgroundWorkerTask> Tasks { get; set; } = new List<BackgroundWorkerTask>();

    public DateTime CreatedAt { get; set; }
}
