#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ClassificationFeedbackEventType
{
    [PgName("LABEL_ADDED")]
    LabelAdded,

    [PgName("LABEL_REMOVED")]
    LabelRemoved,
}
