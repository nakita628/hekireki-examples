#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum LangyProjectionTitleSource
{
    [PgName("derived")]
    Derived,

    [PgName("auto")]
    Auto,

    [PgName("user")]
    User,
}
