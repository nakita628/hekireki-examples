#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum BillingPeriod
{
    [PgName("MONTHLY")]
    Monthly,

    [PgName("ANNUALLY")]
    Annually,
}
