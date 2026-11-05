#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum SystemType
{
    [PgName("TO_REPLY")]
    ToReply,

    [PgName("FYI")]
    Fyi,

    [PgName("AWAITING_REPLY")]
    AwaitingReply,

    [PgName("ACTIONED")]
    Actioned,

    [PgName("COLD_EMAIL")]
    ColdEmail,

    [PgName("NEWSLETTER")]
    Newsletter,

    [PgName("MARKETING")]
    Marketing,

    [PgName("CALENDAR")]
    Calendar,

    [PgName("RECEIPT")]
    Receipt,

    [PgName("NOTIFICATION")]
    Notification,

    [PgName("OTP")]
    Otp,
}
