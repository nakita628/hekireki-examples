#nullable enable

using NpgsqlTypes;

namespace Plunk.Models;

public enum EmailSourceType
{
    [PgName("TRANSACTIONAL")]
    Transactional,

    [PgName("CAMPAIGN")]
    Campaign,

    [PgName("WORKFLOW")]
    Workflow,

    [PgName("INBOUND")]
    Inbound,
}
