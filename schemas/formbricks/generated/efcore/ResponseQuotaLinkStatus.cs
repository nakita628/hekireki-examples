#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum ResponseQuotaLinkStatus
{
    [PgName("screenedIn")]
    ScreenedIn,

    [PgName("screenedOut")]
    ScreenedOut,
}
