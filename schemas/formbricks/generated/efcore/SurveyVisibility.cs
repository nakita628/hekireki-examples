#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum SurveyVisibility
{
    [PgName("restricted")]
    Restricted,

    [PgName("workspace")]
    Workspace,
}
