#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum InvitationStatus
{
    [PgName("SENT")]
    Sent,

    [PgName("FAILED")]
    Failed,

    [PgName("BOUNCED")]
    Bounced,
}
