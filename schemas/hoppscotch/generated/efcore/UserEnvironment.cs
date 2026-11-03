#nullable enable

namespace Hoppscotch.Models;

public partial class UserEnvironment
{
    public string Id { get; set; } = null!;

    public string UserUid { get; set; } = null!;

    public string? Name { get; set; }

    public string Variables { get; set; } = null!;

    public bool IsGlobal { get; set; }

    public virtual User User { get; set; } = null!;
}
