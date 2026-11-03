#nullable enable

namespace NextAuth.Models;

public partial class Authenticator
{
    public string CredentialID { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string ProviderAccountId { get; set; } = null!;

    public string CredentialPublicKey { get; set; } = null!;

    public int Counter { get; set; }

    public string CredentialDeviceType { get; set; } = null!;

    public bool CredentialBackedUp { get; set; }

    public string? Transports { get; set; }

    public virtual User User { get; set; } = null!;
}
