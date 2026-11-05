#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ApiKeyScope
{
    [PgName("STATS_READ")]
    StatsRead,

    [PgName("RULES_READ")]
    RulesRead,

    [PgName("RULES_WRITE")]
    RulesWrite,

    [PgName("SETTINGS_READ")]
    SettingsRead,

    [PgName("SETTINGS_WRITE")]
    SettingsWrite,

    [PgName("ASSISTANT_CHAT")]
    AssistantChat,

    [PgName("SENDERS_UNSUBSCRIBE")]
    SendersUnsubscribe,
}
