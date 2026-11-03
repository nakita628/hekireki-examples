#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum IntegrationService
{
    [PgName("SLACK")]
    Slack,

    [PgName("VERCEL")]
    Vercel,
}
