#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum BlobStorageExportMode
{
    [PgName("FULL_HISTORY")]
    FullHistory,

    [PgName("FROM_TODAY")]
    FromToday,

    [PgName("FROM_CUSTOM_DATE")]
    FromCustomDate,
}
