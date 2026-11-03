#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum InAppAgentConversationVisibilityScope
{
    [PgName("PERSONAL")]
    Personal,

    [PgName("PROJECT")]
    Project,
}
