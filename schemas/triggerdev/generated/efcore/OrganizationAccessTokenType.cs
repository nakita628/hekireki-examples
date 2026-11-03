#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum OrganizationAccessTokenType
{
    [PgName("USER")]
    User,

    [PgName("SYSTEM")]
    System,
}
