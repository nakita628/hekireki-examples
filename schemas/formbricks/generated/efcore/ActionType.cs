#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum ActionType
{
    [PgName("code")]
    Code,

    [PgName("noCode")]
    NoCode,
}
