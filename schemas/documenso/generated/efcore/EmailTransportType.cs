#nullable enable

using NpgsqlTypes;

namespace Documenso.Models;

public enum EmailTransportType
{
    [PgName("SMTP_AUTH")]
    SmtpAuth,

    [PgName("SMTP_API")]
    SmtpApi,

    [PgName("RESEND")]
    Resend,

    [PgName("MAILCHANNELS")]
    Mailchannels,
}
