#nullable enable

using NpgsqlTypes;

namespace Plunk.Models;

public enum EmailStatus
{
    [PgName("SCHEDULED")]
    Scheduled,

    [PgName("CANCELLED")]
    Cancelled,

    [PgName("PENDING")]
    Pending,

    [PgName("SENDING")]
    Sending,

    [PgName("SENT")]
    Sent,

    [PgName("DELIVERED")]
    Delivered,

    [PgName("RECEIVED")]
    Received,

    [PgName("OPENED")]
    Opened,

    [PgName("CLICKED")]
    Clicked,

    [PgName("BOUNCED")]
    Bounced,

    [PgName("COMPLAINED")]
    Complained,

    [PgName("FAILED")]
    Failed,
}
