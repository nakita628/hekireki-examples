#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum AuditLogRecordType
{
    [PgName("USER")]
    User,

    [PgName("API_KEY")]
    ApiKey,
}
