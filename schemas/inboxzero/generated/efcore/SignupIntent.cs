#nullable enable

using NpgsqlTypes;

namespace InboxZero.Models;

public enum SignupIntent
{
    [PgName("AI_ASSISTANT")]
    AiAssistant,

    [PgName("MAIL_CLIENT")]
    MailClient,

    [PgName("BOTH")]
    Both,
}
