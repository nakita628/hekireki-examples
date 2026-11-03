#nullable enable

using NpgsqlTypes;

namespace Postiz.Models;

public enum State
{
    [PgName("QUEUE")]
    Queue,

    [PgName("PUBLISHED")]
    Published,

    [PgName("ERROR")]
    Error,

    [PgName("DRAFT")]
    Draft,
}
