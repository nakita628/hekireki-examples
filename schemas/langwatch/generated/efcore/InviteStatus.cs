#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum InviteStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("ACCEPTED")]
    Accepted,

    [PgName("WAITING_APPROVAL")]
    WaitingApproval,

    [PgName("PAYMENT_PENDING")]
    PaymentPending,

    [PgName("REVOKED")]
    Revoked,
}
