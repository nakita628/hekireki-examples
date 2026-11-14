#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum FeatureFlagExperimentSubjectType
{
    [PgName("USER")]
    User,

    [PgName("ORGANIZATION")]
    Organization,

    [PgName("PROJECT")]
    Project,
}
