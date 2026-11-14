#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum GrantPrincipalType
{
    [PgName("USER")]
    User,

    [PgName("API_KEY")]
    ApiKey,

    [PgName("GROUP")]
    Group,

    [PgName("TEAM")]
    Team,

    [PgName("ORGANIZATION")]
    Organization,

    [PgName("PROJECT")]
    Project,

    [PgName("ANYONE")]
    Anyone,
}
