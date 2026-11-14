#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum PromptScope
{
    [PgName("ORGANIZATION")]
    Organization,

    [PgName("PROJECT")]
    Project,
}
