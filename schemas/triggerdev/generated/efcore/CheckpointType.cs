#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum CheckpointType
{
    [PgName("DOCKER")]
    Docker,

    [PgName("KUBERNETES")]
    Kubernetes,
}
