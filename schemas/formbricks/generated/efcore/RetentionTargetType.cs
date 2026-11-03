#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum RetentionTargetType
{
    [PgName("survey")]
    Survey,

    [PgName("user")]
    User,
}
