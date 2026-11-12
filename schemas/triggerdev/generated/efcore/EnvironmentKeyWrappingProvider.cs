#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum EnvironmentKeyWrappingProvider
{
    [PgName("PLATFORM_KEYRING")]
    PlatformKeyring,

    [PgName("AWS_KMS")]
    AwsKms,
}
