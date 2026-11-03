#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum EmailDomainStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("ACTIVE")]
    Active,
}
