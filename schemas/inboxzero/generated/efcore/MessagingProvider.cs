#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum MessagingProvider
{
    [PgName("SLACK")]
    Slack,

    [PgName("TEAMS")]
    Teams,

    [PgName("TELEGRAM")]
    Telegram,
}
