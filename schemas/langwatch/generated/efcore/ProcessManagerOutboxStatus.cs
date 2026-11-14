#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum ProcessManagerOutboxStatus
{
    [PgName("pending")]
    Pending,

    [PgName("dispatched")]
    Dispatched,

    [PgName("dead")]
    Dead,

    [PgName("discarded")]
    Discarded,
}
