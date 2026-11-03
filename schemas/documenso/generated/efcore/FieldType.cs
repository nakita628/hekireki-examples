#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum FieldType
{
    [PgName("SIGNATURE")]
    Signature,

    [PgName("FREE_SIGNATURE")]
    FreeSignature,

    [PgName("INITIALS")]
    Initials,

    [PgName("NAME")]
    Name,

    [PgName("EMAIL")]
    Email,

    [PgName("DATE")]
    Date,

    [PgName("TEXT")]
    Text,

    [PgName("NUMBER")]
    Number,

    [PgName("RADIO")]
    Radio,

    [PgName("CHECKBOX")]
    Checkbox,

    [PgName("DROPDOWN")]
    Dropdown,
}
