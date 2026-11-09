#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum MailSetupState
{
    [PgName("PENDING")]
    Pending,

    [PgName("SUGGESTED")]
    Suggested,

    [PgName("DONE")]
    Done,
}
