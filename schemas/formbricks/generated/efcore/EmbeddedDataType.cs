#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum EmbeddedDataType
{
    [PgName("string")]
    String,

    [PgName("number")]
    Number,

    [PgName("boolean")]
    Boolean,

    [PgName("date")]
    Date,
}
