#nullable enable

using System;
using System.Collections.Generic;

namespace Trigger.Models;

public partial class WorkerInstanceGroup
{
    public string Id { get; set; } = null!;

    public WorkerInstanceGroupType Type { get; set; }

    public string Name { get; set; } = null!;

    public string MasterQueue { get; set; } = null!;

    public string? Description { get; set; }

    public bool Hidden { get; set; } = false;

    public virtual WorkerGroupToken Token { get; set; } = null!;

    public string TokenId { get; set; } = null!;

    public virtual ICollection<WorkerInstance> Workers { get; set; } = new List<WorkerInstance>();

    public virtual ICollection<BackgroundWorker> BackgroundWorkers { get; set; } = new List<BackgroundWorker>();

    public virtual ICollection<Project> DefaultForProjects { get; set; } = new List<Project>();

    public virtual Organization? Organization { get; set; }

    public string? OrganizationId { get; set; }

    public virtual Project? Project { get; set; }

    public string? ProjectId { get; set; }

    public string? CloudProvider { get; set; }

    public string? Location { get; set; }

    public string? StaticIPs { get; set; }

    public WorkloadType WorkloadType { get; set; } = WorkloadType.Container;

    public string? Region { get; set; }

    public bool EnableFastPath { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
