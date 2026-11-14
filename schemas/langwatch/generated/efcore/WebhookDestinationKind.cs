#nullable enable

using NpgsqlTypes;

namespace LangWatch.Models;

public enum WebhookDestinationKind
{
    [PgName("http")]
    Http,

    [PgName("sqs")]
    Sqs,
}
