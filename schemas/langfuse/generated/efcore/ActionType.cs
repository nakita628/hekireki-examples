#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum ActionType
{
    [PgName("WEBHOOK")]
    Webhook,

    [PgName("SLACK")]
    Slack,

    [PgName("GITHUB_DISPATCH")]
    GithubDispatch,

    [PgName("PAGERDUTY")]
    Pagerduty,
}
