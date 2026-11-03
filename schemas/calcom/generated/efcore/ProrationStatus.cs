#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum ProrationStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("INVOICE_CREATED")]
    InvoiceCreated,

    [PgName("CHARGED")]
    Charged,

    [PgName("FAILED")]
    Failed,

    [PgName("CANCELLED")]
    Cancelled,
}
