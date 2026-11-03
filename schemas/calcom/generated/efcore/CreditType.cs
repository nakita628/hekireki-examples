#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum CreditType
{
    [PgName("MONTHLY")]
    Monthly,

    [PgName("ADDITIONAL")]
    Additional,
}
