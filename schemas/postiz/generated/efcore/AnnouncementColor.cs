#nullable enable

using NpgsqlTypes;

namespace Postiz.Models;

public enum AnnouncementColor
{
    [PgName("INFO")]
    Info,

    [PgName("WARNING")]
    Warning,

    [PgName("ERROR")]
    Error,
}
