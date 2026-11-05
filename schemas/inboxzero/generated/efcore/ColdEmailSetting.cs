#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ColdEmailSetting
{
    [PgName("DISABLED")]
    Disabled,

    [PgName("LIST")]
    List,

    [PgName("LABEL")]
    Label,

    [PgName("ARCHIVE_AND_LABEL")]
    ArchiveAndLabel,

    [PgName("ARCHIVE_AND_READ_AND_LABEL")]
    ArchiveAndReadAndLabel,
}
