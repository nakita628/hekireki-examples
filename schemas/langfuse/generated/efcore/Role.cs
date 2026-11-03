#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum Role
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

    [PgName("AI_GATEWAY")]
    AiGateway,
}
