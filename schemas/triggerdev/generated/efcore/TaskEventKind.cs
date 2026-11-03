#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum TaskEventKind
{
    [PgName("UNSPECIFIED")]
    Unspecified,

    [PgName("INTERNAL")]
    Internal,

    [PgName("SERVER")]
    Server,

    [PgName("CLIENT")]
    Client,

    [PgName("PRODUCER")]
    Producer,

    [PgName("CONSUMER")]
    Consumer,

    [PgName("UNRECOGNIZED")]
    Unrecognized,

    [PgName("LOG")]
    Log,
}
