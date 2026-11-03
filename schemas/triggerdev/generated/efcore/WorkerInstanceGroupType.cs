#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum WorkerInstanceGroupType
{
    [PgName("MANAGED")]
    Managed,

    [PgName("UNMANAGED")]
    Unmanaged,
}
