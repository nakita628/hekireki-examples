package triggerdev.models

enum class CustomerQuerySource(
    val dbName: String,
) {
    DASHBOARD("DASHBOARD"),
    API("API"),
}
