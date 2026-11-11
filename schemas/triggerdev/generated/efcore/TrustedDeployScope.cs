#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum TrustedDeployScope
{
    [PgName("ANY")]
    Any,

    [PgName("BRANCH")]
    Branch,

    [PgName("TAG")]
    Tag,

    [PgName("ENVIRONMENT")]
    Environment,
}
