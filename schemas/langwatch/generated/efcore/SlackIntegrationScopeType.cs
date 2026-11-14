#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum SlackIntegrationScopeType
{
    [PgName("ORGANIZATION")]
    Organization,

    [PgName("PROJECT")]
    Project,
}
