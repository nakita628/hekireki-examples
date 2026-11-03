package documenso.models

enum class SendStatus(
    val dbName: String,
) {
    NOT_SENT("NOT_SENT"),
    SENT("SENT"),
}
