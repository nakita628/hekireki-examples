#nullable enable

namespace Postiz.Models;

public partial class ItemUser
{
    public string Id { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string Key { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
