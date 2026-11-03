#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum WebhookEndpointStatus
{
    [PgName("ACTIVE")]
    Active,

    [PgName("INACTIVE")]
    Inactive,

    [PgName("DELETING")]
    Deleting,
}
