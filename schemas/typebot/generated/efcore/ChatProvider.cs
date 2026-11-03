#nullable enable

using NpgsqlTypes;

namespace Typebot.Models;

public enum ChatProvider
{
    [PgName("WHATSAPP")]
    Whatsapp,

    [PgName("DIALOG360")]
    Dialog360,
}
