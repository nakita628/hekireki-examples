#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum CustomFieldType
{
    [PgName("SHORT_TEXT")]
    ShortText,

    [PgName("LONG_TEXT")]
    LongText,

    [PgName("NUMBER")]
    Number,

    [PgName("PHONE_NUMBER")]
    PhoneNumber,

    [PgName("URL")]
    Url,

    [PgName("CHECKBOX")]
    Checkbox,

    [PgName("SELECT")]
    Select,

    [PgName("MULTI_SELECT")]
    MultiSelect,
}
