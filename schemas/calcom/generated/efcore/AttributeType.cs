#nullable enable

using NpgsqlTypes;

namespace Calcom.Models;

public enum AttributeType
{
    [PgName("TEXT")]
    Text,

    [PgName("NUMBER")]
    Number,

    [PgName("SINGLE_SELECT")]
    SingleSelect,

    [PgName("MULTI_SELECT")]
    MultiSelect,
}
