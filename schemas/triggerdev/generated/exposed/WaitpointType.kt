package triggerdev.models

enum class WaitpointType(
    val dbName: String,
) {
    RUN("RUN"),
    DATETIME("DATETIME"),
    MANUAL("MANUAL"),
    BATCH("BATCH"),
}
