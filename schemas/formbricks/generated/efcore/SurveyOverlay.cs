#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum SurveyOverlay
{
    [PgName("none")]
    None,

    [PgName("light")]
    Light,

    [PgName("dark")]
    Dark,
}
