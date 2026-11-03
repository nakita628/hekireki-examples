#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum ReferralTrack
{
    [PgName("AFFILIATE")]
    Affiliate,

    [PgName("PARTNER_DISCOUNT")]
    PartnerDiscount,
}
