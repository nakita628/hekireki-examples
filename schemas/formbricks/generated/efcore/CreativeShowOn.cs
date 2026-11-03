#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum CreativeShowOn
{
    [PgName("pageView")]
    PageView,

    [PgName("action")]
    Action,
}
