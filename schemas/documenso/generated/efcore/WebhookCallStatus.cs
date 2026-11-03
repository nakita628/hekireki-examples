#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum WebhookCallStatus
{
    [PgName("SUCCESS")]
    Success,

    [PgName("FAILED")]
    Failed,
}
