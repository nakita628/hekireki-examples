#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum FaqVisibility
{
    [PgName("PUBLIC_DATAROOM")]
    PublicDataroom,

    [PgName("PUBLIC_LINK")]
    PublicLink,

    [PgName("PUBLIC_DOCUMENT")]
    PublicDocument,
}
