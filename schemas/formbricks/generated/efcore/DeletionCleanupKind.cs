#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum DeletionCleanupKind
{
    [PgName("hubSurvey")]
    HubSurvey,

    [PgName("hubResponses")]
    HubResponses,

    [PgName("storageFiles")]
    StorageFiles,

    [PgName("storageSurveyFolder")]
    StorageSurveyFolder,

    [PgName("hubResponseOutcome")]
    HubResponseOutcome,
}
