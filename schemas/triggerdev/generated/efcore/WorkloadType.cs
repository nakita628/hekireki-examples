#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum WorkloadType
{
    [PgName("CONTAINER")]
    Container,

    [PgName("MICROVM")]
    Microvm,
}
