#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum TaskRunCheckpointType
{
    [PgName("DOCKER")]
    Docker,

    [PgName("KUBERNETES")]
    Kubernetes,

    [PgName("COMPUTE")]
    Compute,
}
