#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ProcessorType
{
    [PgName("LEMON_SQUEEZY")]
    LemonSqueezy,

    [PgName("STRIPE")]
    Stripe,
}
