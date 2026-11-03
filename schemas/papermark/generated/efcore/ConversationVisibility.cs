#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum ConversationVisibility
{
    [PgName("PRIVATE")]
    Private,

    [PgName("PUBLIC_LINK")]
    PublicLink,

    [PgName("PUBLIC_GROUP")]
    PublicGroup,

    [PgName("PUBLIC_DOCUMENT")]
    PublicDocument,

    [PgName("PUBLIC_DATAROOM")]
    PublicDataroom,
}
