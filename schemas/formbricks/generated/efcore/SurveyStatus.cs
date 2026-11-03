#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum SurveyStatus
{
    [PgName("draft")]
    Draft,

    [PgName("inProgress")]
    InProgress,

    [PgName("paused")]
    Paused,

    [PgName("completed")]
    Completed,
}
