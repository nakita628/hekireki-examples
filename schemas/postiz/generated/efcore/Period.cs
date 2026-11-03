#nullable enable

using NpgsqlTypes;

namespace Postiz.Models;

public enum Period
{
    [PgName("MONTHLY")]
    Monthly,

    [PgName("YEARLY")]
    Yearly,
}
