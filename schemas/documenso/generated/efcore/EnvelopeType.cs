#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum EnvelopeType
{
    [PgName("DOCUMENT")]
    Document,

    [PgName("TEMPLATE")]
    Template,
}
