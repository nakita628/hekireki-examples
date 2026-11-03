#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum ProjectAlertChannelType
{
    [PgName("EMAIL")]
    Email,

    [PgName("SLACK")]
    Slack,

    [PgName("WEBHOOK")]
    Webhook,
}
