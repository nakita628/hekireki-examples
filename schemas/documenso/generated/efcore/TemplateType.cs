#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum TemplateType
{
    [PgName("PUBLIC")]
    Public,

    [PgName("PRIVATE")]
    Private,

    [PgName("ORGANISATION")]
    Organisation,
}
