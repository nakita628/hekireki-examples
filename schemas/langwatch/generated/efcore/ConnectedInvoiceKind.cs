#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum ConnectedInvoiceKind
{
    [PgName("annual")]
    Annual,

    [PgName("seat_change")]
    SeatChange,

    [PgName("usage")]
    Usage,
}
