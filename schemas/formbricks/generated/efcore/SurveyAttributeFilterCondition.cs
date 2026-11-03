#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum SurveyAttributeFilterCondition
{
    [PgName("equals")]
    Equals,

    [PgName("notEquals")]
    NotEquals,
}
