#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum ConversationActivityKind
{
    [PgName("INVITED")]
    Invited,

    [PgName("COMMENT")]
    Comment,

    [PgName("MENTION")]
    Mention,
}
