#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum TimeFormat
{
    [PgName("hours12")]
    Hours12,

    [PgName("hours24")]
    Hours24,
}
