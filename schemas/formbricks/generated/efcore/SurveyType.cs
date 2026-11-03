#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum SurveyType
{
    [PgName("link")]
    Link,

    [PgName("app")]
    App,
}
