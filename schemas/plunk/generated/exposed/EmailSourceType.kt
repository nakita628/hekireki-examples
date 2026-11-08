package plunk.models

enum class EmailSourceType(
    val dbName: String,
) {
    TRANSACTIONAL("TRANSACTIONAL"),
    CAMPAIGN("CAMPAIGN"),
    WORKFLOW("WORKFLOW"),
    INBOUND("INBOUND"),
}
