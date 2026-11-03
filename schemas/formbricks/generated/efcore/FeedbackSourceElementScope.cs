#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum FeedbackSourceElementScope
{
    [PgName("all")]
    All,

    [PgName("specific")]
    Specific,
}
