#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum DownloadType
{
    [PgName("SINGLE")]
    Single,

    [PgName("BULK")]
    Bulk,

    [PgName("FOLDER")]
    Folder,
}
