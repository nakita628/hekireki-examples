#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum WebhookDeliveryStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("PROCESSING")]
    Processing,

    [PgName("SUCCEEDED")]
    Succeeded,

    [PgName("FAILED")]
    Failed,

    [PgName("FILTERED")]
    Filtered,

    [PgName("UNMATCHED")]
    Unmatched,
}
