#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum NewsletterStatus
{
    [PgName("APPROVED")]
    Approved,

    [PgName("UNSUBSCRIBED")]
    Unsubscribed,

    [PgName("AUTO_ARCHIVED")]
    AutoArchived,
}
