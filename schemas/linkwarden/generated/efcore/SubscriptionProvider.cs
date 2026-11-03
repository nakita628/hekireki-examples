#nullable enable

using NpgsqlTypes;

namespace Linkwarden.Models;

public enum SubscriptionProvider
{
    [PgName("STRIPE")]
    Stripe,

    [PgName("APPLE")]
    Apple,

    [PgName("GOOGLE")]
    Google,
}
