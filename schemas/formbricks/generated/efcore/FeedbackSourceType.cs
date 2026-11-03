#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum FeedbackSourceType
{
    [PgName("formbricks_survey")]
    FormbricksSurvey,

    [PgName("csv")]
    Csv,
}
