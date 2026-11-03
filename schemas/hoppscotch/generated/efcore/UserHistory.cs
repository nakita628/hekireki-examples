#nullable enable

using System;

namespace Hoppscotch.Models;

public partial class UserHistory
{
    public string Id { get; set; } = null!;

    public string UserUid { get; set; } = null!;

    public ReqType ReqType { get; set; }

    public string Request { get; set; } = null!;

    public string ResponseMetadata { get; set; } = null!;

    public bool IsStarred { get; set; }

    public DateTime ExecutedOn { get; set; }

    public virtual User User { get; set; } = null!;
}
