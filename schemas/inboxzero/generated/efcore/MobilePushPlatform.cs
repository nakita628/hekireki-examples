#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum MobilePushPlatform
{
    [PgName("android")]
    Android,

    [PgName("ios")]
    Ios,
}
