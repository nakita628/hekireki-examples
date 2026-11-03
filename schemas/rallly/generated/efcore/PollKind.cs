#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum PollKind
{
    [PgName("date")]
    Date,

    [PgName("time")]
    Time,
}
