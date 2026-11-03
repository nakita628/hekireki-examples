#nullable enable

using NpgsqlTypes;

namespace Trigger.Models;

public enum GithubRepositorySelection
{
    [PgName("ALL")]
    All,

    [PgName("SELECTED")]
    Selected,
}
