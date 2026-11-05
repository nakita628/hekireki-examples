#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum MessagingMessageStatus
{
    [PgName("SENT")]
    Sent,

    [PgName("DRAFT_SENT")]
    DraftSent,

    [PgName("DRAFT_EDITED")]
    DraftEdited,

    [PgName("DISMISSED")]
    Dismissed,

    [PgName("EXPIRED")]
    Expired,

    [PgName("FAILED")]
    Failed,
}
