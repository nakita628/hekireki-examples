#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum FeedbackSourceStatus
{
    [PgName("active")]
    Active,

    [PgName("paused")]
    Paused,

    [PgName("error")]
    Error,
}
