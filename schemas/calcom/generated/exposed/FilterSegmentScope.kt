package calcom.models

enum class FilterSegmentScope(
    val dbName: String,
) {
    USER("USER"),
    TEAM("TEAM"),
}
