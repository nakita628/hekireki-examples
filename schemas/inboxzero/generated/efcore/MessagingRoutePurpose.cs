#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum MessagingRoutePurpose
{
    [PgName("RULE_NOTIFICATIONS")]
    RuleNotifications,

    [PgName("SCHEDULED_CHECK_INS")]
    ScheduledCheckIns,

    [PgName("MEETING_BRIEFS")]
    MeetingBriefs,

    [PgName("DOCUMENT_FILINGS")]
    DocumentFilings,

    [PgName("DIGESTS")]
    Digests,

    [PgName("FOLLOW_UPS")]
    FollowUps,
}
