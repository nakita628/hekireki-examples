#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum HubFieldType
{
    [PgName("text")]
    Text,

    [PgName("categorical")]
    Categorical,

    [PgName("nps")]
    Nps,

    [PgName("csat")]
    Csat,

    [PgName("ces")]
    Ces,

    [PgName("rating")]
    Rating,

    [PgName("number")]
    Number,

    [PgName("boolean")]
    Boolean,

    [PgName("date")]
    Date,
}
