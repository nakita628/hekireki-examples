#nullable enable

using NpgsqlTypes;

namespace WorkoutCool.Models;

public enum PaymentProcessor
{
    [PgName("STRIPE")]
    Stripe,

    [PgName("PAYPAL")]
    Paypal,

    [PgName("LEMONSQUEEZY")]
    Lemonsqueezy,

    [PgName("PADDLE")]
    Paddle,

    [PgName("APPLE_PAY")]
    ApplePay,

    [PgName("GOOGLE_PAY")]
    GooglePay,

    [PgName("REVENUECAT")]
    Revenuecat,

    [PgName("NONE")]
    None,

    [PgName("OTHER")]
    Other,
}
