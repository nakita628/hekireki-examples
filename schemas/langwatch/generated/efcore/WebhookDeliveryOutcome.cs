#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum WebhookDeliveryOutcome
{
    [PgName("success")]
    Success,

    [PgName("retryable")]
    Retryable,

    [PgName("terminal")]
    Terminal,

    [PgName("pending")]
    Pending,
}
