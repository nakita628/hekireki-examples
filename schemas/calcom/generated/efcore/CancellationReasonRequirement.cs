#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum CancellationReasonRequirement
{
    [PgName("MANDATORY_BOTH")]
    MandatoryBoth,

    [PgName("MANDATORY_HOST_ONLY")]
    MandatoryHostOnly,

    [PgName("MANDATORY_ATTENDEE_ONLY")]
    MandatoryAttendeeOnly,

    [PgName("OPTIONAL_BOTH")]
    OptionalBoth,
}
