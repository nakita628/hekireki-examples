#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum FeedbackSourceImportMode
{
    [PgName("completedOnly")]
    CompletedOnly,

    [PgName("all")]
    All,
}
