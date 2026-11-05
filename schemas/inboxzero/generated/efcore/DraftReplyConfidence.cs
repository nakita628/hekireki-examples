#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum DraftReplyConfidence
{
    [PgName("ALL_EMAILS")]
    AllEmails,

    [PgName("STANDARD")]
    Standard,

    [PgName("HIGH_CONFIDENCE")]
    HighConfidence,
}
