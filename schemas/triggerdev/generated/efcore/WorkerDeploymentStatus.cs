#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum WorkerDeploymentStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("INSTALLING")]
    Installing,

    [PgName("BUILDING")]
    Building,

    [PgName("DEPLOYING")]
    Deploying,

    [PgName("DEPLOYED")]
    Deployed,

    [PgName("FAILED")]
    Failed,

    [PgName("CANCELED")]
    Canceled,

    [PgName("TIMED_OUT")]
    TimedOut,
}
