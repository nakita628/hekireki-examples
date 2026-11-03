#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum AssignmentReasonEnum
{
    [PgName("REASSIGNED")]
    Reassigned,

    [PgName("RR_REASSIGNED")]
    RrReassigned,

    [PgName("REROUTED")]
    Rerouted,

    [PgName("SALESFORCE_ASSIGNMENT")]
    SalesforceAssignment,
}
