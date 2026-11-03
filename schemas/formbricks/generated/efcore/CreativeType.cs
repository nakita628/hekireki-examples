#nullable enable

using NpgsqlTypes;

namespace Formbricks.Models;

public enum CreativeType
{
    [PgName("feedbackButton")]
    FeedbackButton,

    [PgName("banner")]
    Banner,
}
