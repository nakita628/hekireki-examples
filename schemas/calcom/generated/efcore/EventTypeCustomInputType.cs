#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum EventTypeCustomInputType
{
    [PgName("text")]
    Text,

    [PgName("textLong")]
    Textlong,

    [PgName("number")]
    Number,

    [PgName("bool")]
    Bool,

    [PgName("radio")]
    Radio,

    [PgName("phone")]
    Phone,
}
