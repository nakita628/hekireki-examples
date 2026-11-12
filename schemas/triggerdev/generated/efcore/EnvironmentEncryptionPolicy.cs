#nullable enable

namespace Trigger.Models;

public partial class EnvironmentEncryptionPolicy
{
    public string EnvironmentId { get; set; } = null!;

    public virtual RuntimeEnvironment Environment { get; set; } = null!;

    public int Revision { get; set; } = 0;

    public EnvironmentKeyWrappingProvider? WrappingProvider { get; set; }

    public string? WrappingKeyReference { get; set; }

    public string? ActiveKeyId { get; set; }

    public virtual EnvironmentEncryptionKey? ActiveKey { get; set; }
}
