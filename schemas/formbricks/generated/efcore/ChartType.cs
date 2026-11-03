#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum ChartType
{
    [PgName("area")]
    Area,

    [PgName("bar")]
    Bar,

    [PgName("pie")]
    Pie,

    [PgName("big_number")]
    BigNumber,

    [PgName("matrix")]
    Matrix,
}
