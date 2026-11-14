#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum ConnectedSeatChangeState
{
    [PgName("intent")]
    Intent,

    [PgName("invoiced")]
    Invoiced,

    [PgName("nothing_to_invoice")]
    NothingToInvoice,

    [PgName("not_onboarded")]
    NotOnboarded,
}
