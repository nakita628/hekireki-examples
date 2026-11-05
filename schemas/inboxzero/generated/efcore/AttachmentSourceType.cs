#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum AttachmentSourceType
{
    [PgName("FILE")]
    File,

    [PgName("FOLDER")]
    Folder,
}
