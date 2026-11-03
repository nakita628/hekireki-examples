#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum MediaAssociationOrigin
{
    [PgName("UNKNOWN")]
    Unknown,

    [PgName("CLIENT_UPLOAD")]
    ClientUpload,

    [PgName("INGESTION_MEDIA_EXTRACTION")]
    IngestionMediaExtraction,

    [PgName("INGESTION_FIELD_OVERFLOW")]
    IngestionFieldOverflow,
}
