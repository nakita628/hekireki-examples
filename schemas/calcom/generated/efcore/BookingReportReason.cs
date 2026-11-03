#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum BookingReportReason
{
    [PgName("SPAM")]
    Spam,

    [PgName("dont_know_person")]
    DontKnowPerson,

    [PgName("OTHER")]
    Other,
}
