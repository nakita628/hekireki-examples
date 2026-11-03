#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum BlobStorageIntegrationFileType
{
    [PgName("JSON")]
    Json,

    [PgName("CSV")]
    Csv,

    [PgName("JSONL")]
    Jsonl,

    [PgName("PARQUET")]
    Parquet,
}
