package documenso.models

enum class SigningStatus(
    val dbName: String,
) {
    NOT_SIGNED("NOT_SIGNED"),
    SIGNED("SIGNED"),
    REJECTED("REJECTED"),
}
