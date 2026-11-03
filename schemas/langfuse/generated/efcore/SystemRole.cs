#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum SystemRole
{
    [PgName("OWNER")]
    Owner,

    [PgName("ADMIN")]
    Admin,

    [PgName("MEMBER")]
    Member,

    [PgName("VIEWER")]
    Viewer,

    [PgName("NONE")]
    None,

    [PgName("LEGACY_PROJECT_API_KEY")]
    LegacyProjectApiKey,

    [PgName("LEGACY_ORGANIZATION_API_KEY")]
    LegacyOrganizationApiKey,

    [PgName("SCORES_INGEST")]
    ScoresIngest,

    [PgName("INGEST")]
    Ingest,

    [PgName("AI_GATEWAY")]
    AiGateway,
}
