#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum IssuedLicenseSource
{
    [PgName("BACKOFFICE")]
    Backoffice,

    [PgName("PURCHASE")]
    Purchase,

    [PgName("SCRIPT")]
    Script,

    [PgName("LEGACY_IMPORT")]
    LegacyImport,
}
