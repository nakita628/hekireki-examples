#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum CustomLLMModelCostScopeType
{
    [PgName("ORGANIZATION")]
    Organization,

    [PgName("TEAM")]
    Team,

    [PgName("PROJECT")]
    Project,
}
