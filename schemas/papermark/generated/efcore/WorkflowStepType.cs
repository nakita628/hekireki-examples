#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum WorkflowStepType
{
    [PgName("ROUTER")]
    Router,
}
