#nullable enable

using NpgsqlTypes;

namespace Rallly.Models;

public enum VoteType
{
    [PgName("yes")]
    Yes,

    [PgName("no")]
    No,

    [PgName("ifNeedBe")]
    IfNeedBe,
}
