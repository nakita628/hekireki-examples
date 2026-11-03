#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum SupportAccessMode
{
    [PgName("ALLOW")]
    Allow,

    [PgName("REQUIRES_REQUEST")]
    RequiresRequest,
}
