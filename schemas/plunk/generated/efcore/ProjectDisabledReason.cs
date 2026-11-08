#nullable enable

using NpgsqlTypes;

namespace Plunk.Models;

public enum ProjectDisabledReason
{
    [PgName("PAYMENT_FAILED")]
    PaymentFailed,

    [PgName("EMAIL_REPUTATION")]
    EmailReputation,

    [PgName("PHISHING_DETECTED")]
    PhishingDetected,

    [PgName("CARD_VERIFICATION_FAILED")]
    CardVerificationFailed,

    [PgName("CARD_VERIFICATION_REQUIRED")]
    CardVerificationRequired,

    [PgName("MANUAL")]
    Manual,
}
