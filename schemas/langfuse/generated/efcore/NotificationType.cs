#nullable enable

using NpgsqlTypes;

namespace Langfuse.Models;

public enum NotificationType
{
    [PgName("COMMENT_MENTION")]
    CommentMention,
}
