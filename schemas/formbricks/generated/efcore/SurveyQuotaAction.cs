#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum SurveyQuotaAction
{
    [PgName("endSurvey")]
    EndSurvey,

    [PgName("continueSurvey")]
    ContinueSurvey,
}
