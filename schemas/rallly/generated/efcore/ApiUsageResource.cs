#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum ApiUsageResource
{
    [PgName("poll")]
    Poll,
}
