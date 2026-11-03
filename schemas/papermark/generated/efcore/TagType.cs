#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum TagType
{
    [PgName("LINK_TAG")]
    LinkTag,

    [PgName("DOCUMENT_TAG")]
    DocumentTag,

    [PgName("DATAROOM_TAG")]
    DataroomTag,
}
