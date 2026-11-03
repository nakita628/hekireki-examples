#nullable enable

using NpgsqlTypes;

namespace Typebot.Models;

public enum GraphNavigation
{
    [PgName("MOUSE")]
    Mouse,

    [PgName("TRACKPAD")]
    Trackpad,
}
