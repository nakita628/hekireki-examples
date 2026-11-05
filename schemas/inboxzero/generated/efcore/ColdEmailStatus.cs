#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ColdEmailStatus
{
    [PgName("AI_LABELED_COLD")]
    AiLabeledCold,

    [PgName("USER_REJECTED_COLD")]
    UserRejectedCold,
}
