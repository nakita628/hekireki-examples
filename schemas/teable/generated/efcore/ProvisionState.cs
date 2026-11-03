#nullable enable

using NpgsqlTypes;

namespace Teable.Models;

public enum ProvisionState
{
    [PgName("pending")]
    Pending,

    [PgName("ready")]
    Ready,

    [PgName("error")]
    Error,

    [PgName("deleting")]
    Deleting,
}
