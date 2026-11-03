#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum CustomerQuerySource
{
    [PgName("DASHBOARD")]
    Dashboard,

    [PgName("API")]
    Api,
}
