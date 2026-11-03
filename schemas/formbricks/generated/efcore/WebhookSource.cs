#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum WebhookSource
{
    [PgName("user")]
    User,

    [PgName("zapier")]
    Zapier,

    [PgName("make")]
    Make,

    [PgName("n8n")]
    N8n,

    [PgName("activepieces")]
    Activepieces,
}
