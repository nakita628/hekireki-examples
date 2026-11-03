#nullable enable

using NpgsqlTypes;

namespace Papermark.Models;

public enum RootItemAccess
{
    [PgName("VIEW_ONLY")]
    ViewOnly,

    [PgName("VIEW_AND_DOWNLOAD")]
    ViewAndDownload,

    [PgName("HIDDEN")]
    Hidden,
}
