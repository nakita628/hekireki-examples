#nullable enable

using System;

namespace Hoppscotch.Models;

public partial class UserSettings
{
    public string Id { get; set; } = null!;

    public string UserUid { get; set; } = null!;

    public string Properties { get; set; } = null!;

    public DateTime UpdatedOn { get; set; }

    public virtual User User { get; set; } = null!;
}
