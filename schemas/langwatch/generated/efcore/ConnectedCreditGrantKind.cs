#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum ConnectedCreditGrantKind
{
    [PgName("commit")]
    Commit,

    [PgName("added")]
    Added,

    [PgName("renewal")]
    Renewal,
}
