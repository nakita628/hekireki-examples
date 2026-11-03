#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum SurveyScriptMode
{
    [PgName("add")]
    Add,

    [PgName("replace")]
    Replace,
}
