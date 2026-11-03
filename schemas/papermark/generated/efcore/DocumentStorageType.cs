#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum DocumentStorageType
{
    [PgName("S3_PATH")]
    S3Path,

    [PgName("VERCEL_BLOB")]
    VercelBlob,
}
