#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum StorageIntegrationAuthType
{
    [PgName("ACCESS_KEYS")]
    AccessKeys,

    [PgName("ROLE_ARN")]
    RoleArn,
}
