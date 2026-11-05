#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ActionType
{
    [PgName("ARCHIVE")]
    Archive,

    [PgName("LABEL")]
    Label,

    [PgName("REPLY")]
    Reply,

    [PgName("SEND_EMAIL")]
    SendEmail,

    [PgName("FORWARD")]
    Forward,

    [PgName("DRAFT_EMAIL")]
    DraftEmail,

    [PgName("DRAFT_MESSAGING_CHANNEL")]
    DraftMessagingChannel,

    [PgName("NOTIFY_MESSAGING_CHANNEL")]
    NotifyMessagingChannel,

    [PgName("MARK_SPAM")]
    MarkSpam,

    [PgName("CALL_WEBHOOK")]
    CallWebhook,

    [PgName("MARK_READ")]
    MarkRead,

    [PgName("STAR")]
    Star,

    [PgName("DELETE")]
    Delete,

    [PgName("DIGEST")]
    Digest,

    [PgName("MOVE_FOLDER")]
    MoveFolder,

    [PgName("NOTIFY_SENDER")]
    NotifySender,

    [PgName("INTEGRATION")]
    Integration,
}
