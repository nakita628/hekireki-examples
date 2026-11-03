#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum PollClosedReason
{
    [PgName("auto")]
    Auto,

    [PgName("manual")]
    Manual,
}
