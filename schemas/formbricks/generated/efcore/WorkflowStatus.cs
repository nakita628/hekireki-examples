#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum WorkflowStatus
{
    [PgName("draft")]
    Draft,

    [PgName("enabled")]
    Enabled,

    [PgName("disabled")]
    Disabled,

    [PgName("archived")]
    Archived,
}
