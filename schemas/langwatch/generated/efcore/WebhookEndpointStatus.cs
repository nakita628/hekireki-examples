#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum WebhookEndpointStatus
{
    [PgName("ACTIVE")]
    Active,

    [PgName("DISABLED")]
    Disabled,
}
