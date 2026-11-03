#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum WidgetPlacement
{
    [PgName("bottomLeft")]
    BottomLeft,

    [PgName("bottomRight")]
    BottomRight,

    [PgName("topLeft")]
    TopLeft,

    [PgName("topRight")]
    TopRight,

    [PgName("center")]
    Center,
}
