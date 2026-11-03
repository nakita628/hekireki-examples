#nullable enable

using NpgsqlTypes;

namespace Linkwarden.Models;

public enum LinksRouteTo
{
    [PgName("ORIGINAL")]
    Original,

    [PgName("PDF")]
    Pdf,

    [PgName("READABLE")]
    Readable,

    [PgName("MONOLITH")]
    Monolith,

    [PgName("SCREENSHOT")]
    Screenshot,

    [PgName("DETAILS")]
    Details,
}
