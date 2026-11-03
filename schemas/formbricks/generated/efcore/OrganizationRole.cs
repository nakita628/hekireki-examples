#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum OrganizationRole
{
    [PgName("owner")]
    Owner,

    [PgName("manager")]
    Manager,

    [PgName("member")]
    Member,

    [PgName("billing")]
    Billing,
}
