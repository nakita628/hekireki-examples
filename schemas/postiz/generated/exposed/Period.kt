package postiz.models

enum class Period(
    val dbName: String,
) {
    MONTHLY("MONTHLY"),
    YEARLY("YEARLY"),
}
