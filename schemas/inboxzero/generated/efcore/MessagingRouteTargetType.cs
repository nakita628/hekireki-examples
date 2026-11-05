#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum MessagingRouteTargetType
{
    [PgName("CHANNEL")]
    Channel,

    [PgName("DIRECT_MESSAGE")]
    DirectMessage,
}
