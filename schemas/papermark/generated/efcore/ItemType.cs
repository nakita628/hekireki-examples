#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum ItemType
{
    [PgName("DATAROOM_DOCUMENT")]
    DataroomDocument,

    [PgName("DATAROOM_FOLDER")]
    DataroomFolder,
}
