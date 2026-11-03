#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum DocumentDataType
{
    [PgName("S3_PATH")]
    S3Path,

    [PgName("BYTES")]
    Bytes,

    [PgName("BYTES_64")]
    Bytes64,
}
