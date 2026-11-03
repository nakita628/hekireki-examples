#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum DocumentStatus
{
    [PgName("DRAFT")]
    Draft,

    [PgName("PENDING")]
    Pending,

    [PgName("COMPLETED")]
    Completed,

    [PgName("REJECTED")]
    Rejected,

    [PgName("CANCELLED")]
    Cancelled,
}
