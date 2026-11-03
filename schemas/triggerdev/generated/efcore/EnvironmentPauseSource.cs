#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum EnvironmentPauseSource
{
    [PgName("BILLING_LIMIT")]
    BillingLimit,
}
