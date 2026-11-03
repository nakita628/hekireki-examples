#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum DisplayOptions
{
    [PgName("displayOnce")]
    DisplayOnce,

    [PgName("displayMultiple")]
    DisplayMultiple,

    [PgName("displaySome")]
    DisplaySome,

    [PgName("respondMultiple")]
    RespondMultiple,
}
