#nullable enable

using NpgsqlTypes;

namespace Hoppscotch.Models;

public enum ReqType
{
    [PgName("REST")]
    Rest,

    [PgName("GQL")]
    Gql,
}
