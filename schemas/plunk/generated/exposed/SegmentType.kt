package plunk.models

enum class SegmentType(
    val dbName: String,
) {
    DYNAMIC("DYNAMIC"),
    STATIC("STATIC"),
}
