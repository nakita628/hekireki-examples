#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum ExternalMediaStorageIntegrationType
{
    [PgName("S3")]
    S3,

    [PgName("S3_COMPATIBLE")]
    S3Compatible,
}
