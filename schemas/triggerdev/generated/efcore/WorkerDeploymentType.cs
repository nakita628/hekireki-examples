#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum WorkerDeploymentType
{
    [PgName("MANAGED")]
    Managed,

    [PgName("UNMANAGED")]
    Unmanaged,

    [PgName("V1")]
    V1,
}
