#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum WebhookDeliveryChannel
{
    [PgName("platform")]
    Platform,

    [PgName("automations")]
    Automations,
}
