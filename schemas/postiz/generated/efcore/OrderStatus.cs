#nullable enable

using NpgsqlTypes;

namespace Postiz.Models;

public enum OrderStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("ACCEPTED")]
    Accepted,

    [PgName("CANCELED")]
    Canceled,

    [PgName("COMPLETED")]
    Completed,
}
