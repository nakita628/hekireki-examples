package langwatch.models

enum class LangyProjectionTitleSource(
    val dbName: String,
) {
    DERIVED("derived"),
    AUTO("auto"),
    USER("user"),
}
