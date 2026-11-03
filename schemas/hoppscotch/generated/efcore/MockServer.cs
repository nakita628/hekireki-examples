#nullable enable

using System;
using System.Collections.Generic;

namespace Hoppscotch.Models;

public partial class MockServer
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Subdomain { get; set; } = null!;

    public string? CreatorUid { get; set; }

    public string CollectionID { get; set; } = null!;

    public WorkspaceType WorkspaceType { get; set; }

    public string WorkspaceID { get; set; } = null!;

    public int DelayInMs { get; set; } = 0;

    public bool IsPublic { get; set; } = true;

    public bool IsActive { get; set; } = true;

    public int HitCount { get; set; } = 0;

    public DateTime? LastHitAt { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<MockServerLog> RequestLogs { get; set; } = new List<MockServerLog>();

    public virtual ICollection<MockServerActivity> ActivityHistory { get; set; } = new List<MockServerActivity>();
}
