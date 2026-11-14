package langwatch.models

enum class CostReferenceType(
    val dbName: String,
) {
    CHECK("CHECK"),
    TRACE("TRACE"),
    PROJECT("PROJECT"),
    BATCH("BATCH"),
}
