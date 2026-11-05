#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum EmailSendOperationStatus
{
    [PgName("PROCESSING")]
    Processing,

    [PgName("SENT")]
    Sent,

    [PgName("UNCERTAIN")]
    Uncertain,
}
