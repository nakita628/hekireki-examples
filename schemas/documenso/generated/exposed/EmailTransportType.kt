package documenso.models

enum class EmailTransportType(
    val dbName: String,
) {
    SMTP_AUTH("SMTP_AUTH"),
    SMTP_API("SMTP_API"),
    RESEND("RESEND"),
    MAILCHANNELS("MAILCHANNELS"),
}
