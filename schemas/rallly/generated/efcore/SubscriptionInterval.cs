#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum SubscriptionInterval
{
    [PgName("month")]
    Month,

    [PgName("year")]
    Year,
}
