#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum RetentionSurveyCondition
{
    [PgName("noResponse")]
    NoResponse,

    [PgName("noChange")]
    NoChange,

    [PgName("createdBefore")]
    CreatedBefore,
}
