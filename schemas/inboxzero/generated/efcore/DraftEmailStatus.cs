#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum DraftEmailStatus
{
    [PgName("PENDING")]
    Pending,

    [PgName("LIKELY_SENT")]
    LikelySent,

    [PgName("REPLIED_WITHOUT_DRAFT")]
    RepliedWithoutDraft,

    [PgName("CLEANED_UP_UNUSED")]
    CleanedUpUnused,

    [PgName("MISSING_FROM_PROVIDER")]
    MissingFromProvider,

    [PgName("USER_EDITED")]
    UserEdited,

    [PgName("CLEANUP_SKIPPED")]
    CleanupSkipped,
}
