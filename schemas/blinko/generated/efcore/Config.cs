#nullable enable

namespace Blinko.Models;

public partial class Config
{
    public int Id { get; set; }

    public string Key { get; set; } = "";

    public string? Config1 { get; set; }

    public int? UserId { get; set; }

    public virtual Accounts? User { get; set; }
}
