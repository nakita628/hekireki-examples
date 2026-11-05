#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum PremiumTier
{
    [PgName("BASIC_MONTHLY")]
    BasicMonthly,

    [PgName("BASIC_ANNUALLY")]
    BasicAnnually,

    [PgName("PRO_MONTHLY")]
    ProMonthly,

    [PgName("PRO_ANNUALLY")]
    ProAnnually,

    [PgName("BUSINESS_MONTHLY")]
    StarterMonthly,

    [PgName("BUSINESS_ANNUALLY")]
    StarterAnnually,

    [PgName("PLUS_MONTHLY")]
    PlusMonthly,

    [PgName("PLUS_ANNUALLY")]
    PlusAnnually,

    [PgName("BUSINESS_PLUS_MONTHLY")]
    ProfessionalMonthly,

    [PgName("BUSINESS_PLUS_ANNUALLY")]
    ProfessionalAnnually,

    [PgName("COPILOT_MONTHLY")]
    CopilotMonthly,

    [PgName("LIFETIME")]
    Lifetime,
}
