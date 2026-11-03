#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum NotificationChannel
{
    [PgName("EMAIL")]
    Email,
}
