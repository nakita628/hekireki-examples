#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum SlackIntegrationKind
{
    [PgName("BOT")]
    Bot,

    [PgName("INCOMING_WEBHOOK")]
    IncomingWebhook,
}
