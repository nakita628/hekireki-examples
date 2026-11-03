#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum ContactAttributeDataType
{
    [PgName("string")]
    String,

    [PgName("number")]
    Number,

    [PgName("date")]
    Date,
}
