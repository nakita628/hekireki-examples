#nullable enable

namespace Calcom.Models;

public partial class UserPassword
{
    public string Hash { get; set; } = null!;

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
