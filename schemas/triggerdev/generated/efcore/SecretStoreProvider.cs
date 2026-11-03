#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum SecretStoreProvider
{
    [PgName("DATABASE")]
    Database,

    [PgName("AWS_PARAM_STORE")]
    AwsParamStore,
}
