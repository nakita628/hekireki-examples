#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum FolderType
{
    [PgName("DOCUMENT")]
    Document,

    [PgName("TEMPLATE")]
    Template,
}
