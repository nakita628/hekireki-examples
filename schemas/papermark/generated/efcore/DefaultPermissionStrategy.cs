#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum DefaultPermissionStrategy
{
    [PgName("INHERIT_FROM_PARENT")]
    InheritFromParent,

    [PgName("ASK_EVERY_TIME")]
    AskEveryTime,

    [PgName("HIDDEN_BY_DEFAULT")]
    HiddenByDefault,
}
