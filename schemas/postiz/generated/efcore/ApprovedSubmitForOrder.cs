#nullable enable

using NpgsqlTypes;

namespace Postiz.Models;

public enum ApprovedSubmitForOrder
{
    [PgName("NO")]
    No,

    [PgName("WAITING_CONFIRMATION")]
    WaitingConfirmation,

    [PgName("YES")]
    Yes,
}
