#nullable enable

using NpgsqlTypes;

namespace Plunk.Models;

public enum CampaignStatus
{
    [PgName("DRAFT")]
    Draft,

    [PgName("SCHEDULED")]
    Scheduled,

    [PgName("SENDING")]
    Sending,

    [PgName("SENT")]
    Sent,

    [PgName("CANCELLED")]
    Cancelled,
}
