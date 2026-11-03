#nullable enable

using NpgsqlTypes;

namespace Postiz.Models;

public enum ShortLinkPreference
{
    [PgName("ASK")]
    Ask,

    [PgName("YES")]
    Yes,

    [PgName("NO")]
    No,
}
