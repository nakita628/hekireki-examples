#nullable enable

namespace TypeHero.Models;

public partial class BetaTokens
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;

    public virtual User? User { get; set; }

    public string? UserId { get; set; }
}
