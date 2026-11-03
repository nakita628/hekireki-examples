#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum RecipientRole
{
    [PgName("CC")]
    Cc,

    [PgName("SIGNER")]
    Signer,

    [PgName("VIEWER")]
    Viewer,

    [PgName("APPROVER")]
    Approver,

    [PgName("ASSISTANT")]
    Assistant,
}
