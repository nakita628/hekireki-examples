#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum GatewayGuardrailDirection
{
    [PgName("PRE")]
    Pre,

    [PgName("POST")]
    Post,

    [PgName("STREAM_CHUNK")]
    StreamChunk,
}
