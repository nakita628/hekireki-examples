#nullable enable

using System;

namespace Hoppscotch.Models;

public partial class UserRequest
{
    public string Id { get; set; } = null!;

    public string CollectionID { get; set; } = null!;

    public string UserUid { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Request { get; set; } = null!;

    public ReqType Type { get; set; }

    public string? MockExamples { get; set; }

    public int OrderIndex { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public virtual UserCollection UserCollection { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
