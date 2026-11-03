#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum OrganisationMemberRole
{
    [PgName("ADMIN")]
    Admin,

    [PgName("MANAGER")]
    Manager,

    [PgName("MEMBER")]
    Member,
}
