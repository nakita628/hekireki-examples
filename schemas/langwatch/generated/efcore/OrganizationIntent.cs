#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum OrganizationIntent
{
    [PgName("AGENT_GOVERNANCE")]
    AgentGovernance,

    [PgName("LLM_OPS")]
    LlmOps,
}
