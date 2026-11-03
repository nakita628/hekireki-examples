#nullable enable

using System;
using System.Collections.Generic;

namespace Teable.Models;

public partial class Space
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Avatar { get; set; }

    public int? Credit { get; set; }

    public DateTime? DeletedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? LastModifiedBy { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public bool? IsTemplate { get; set; }

    public virtual ICollection<Base> BaseGroup { get; set; } = new List<Base>();

    public virtual SpaceDataDbBinding? DataDbBinding { get; set; }

    public virtual ICollection<SpaceDataDbMigrationJob> DataDbMigrationJobs { get; set; } = new List<SpaceDataDbMigrationJob>();

    public virtual ICollection<BaseDataDbMoveJob> BaseDataDbMoveJobsAsSource { get; set; } = new List<BaseDataDbMoveJob>();

    public virtual ICollection<BaseDataDbMoveJob> BaseDataDbMoveJobsAsTarget { get; set; } = new List<BaseDataDbMoveJob>();
}
