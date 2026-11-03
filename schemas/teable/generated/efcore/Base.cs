#nullable enable

using System;
using System.Collections.Generic;

namespace Teable.Models;

public partial class Base
{
    public string Id { get; set; } = null!;

    public string SpaceId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public double Order { get; set; }

    public string? Icon { get; set; }

    public string? SchemaPass { get; set; }

    public bool V2Enabled { get; set; } = false;

    public ProvisionState ProvisionState { get; set; } = ProvisionState.Ready;

    public DateTime? DeletedTime { get; set; }

    public DateTime CreatedTime { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? LastModifiedBy { get; set; }

    public DateTime? LastModifiedTime { get; set; }

    public virtual Space Space { get; set; } = null!;

    public virtual ICollection<TableMeta> Tables { get; set; } = new List<TableMeta>();

    public virtual ICollection<BaseDataDbMoveJob> DataDbMoveJobs { get; set; } = new List<BaseDataDbMoveJob>();

    public virtual ICollection<UserBaseOrder> UserOrders { get; set; } = new List<UserBaseOrder>();
}
