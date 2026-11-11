#nullable enable

using NpgsqlTypes;

namespace LiteLLM.Models;

public enum AccountingDeliveryState
{
    [PgName("pending")]
    Pending,

    [PgName("applied")]
    Applied,

    [PgName("parked")]
    Parked,
}
