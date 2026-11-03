package triggerdev.models

enum class ErrorGroupStatus(
    val dbName: String,
) {
    UNRESOLVED("UNRESOLVED"),
    RESOLVED("RESOLVED"),
    IGNORED("IGNORED"),
}
