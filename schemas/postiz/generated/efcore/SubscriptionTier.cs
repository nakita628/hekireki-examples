#nullable enable

using NpgsqlTypes;

namespace Postiz.Models;

public enum SubscriptionTier
{
    [PgName("STANDARD")]
    Standard,

    [PgName("PRO")]
    Pro,

    [PgName("TEAM")]
    Team,

    [PgName("ULTIMATE")]
    Ultimate,
}
