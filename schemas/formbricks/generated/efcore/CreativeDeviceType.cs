#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum CreativeDeviceType
{
    [PgName("phone")]
    Phone,

    [PgName("tablet")]
    Tablet,

    [PgName("desktop")]
    Desktop,
}
