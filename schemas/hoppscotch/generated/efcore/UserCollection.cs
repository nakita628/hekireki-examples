#nullable enable

using System;
using System.Collections.Generic;

namespace Hoppscotch.Models;

public partial class UserCollection
{
    public string Id { get; set; } = null!;

    public string? ParentID { get; set; }

    public string UserUid { get; set; } = null!;

    public string Title { get; set; } = null!;

    public ReqType Type { get; set; }

    public int OrderIndex { get; set; }

    public string? Data { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public virtual UserCollection? Parent { get; set; }

    public virtual ICollection<UserCollection> Children { get; set; } = new List<UserCollection>();

    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserRequest> Requests { get; set; } = new List<UserRequest>();
}
