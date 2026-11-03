#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum BlobStorageIntegrationType
{
    [PgName("S3")]
    S3,

    [PgName("S3_COMPATIBLE")]
    S3Compatible,

    [PgName("AZURE_BLOB_STORAGE")]
    AzureBlobStorage,

    [PgName("GOOGLE_CLOUD_STORAGE")]
    GoogleCloudStorage,
}
