package plunk.models

enum class TemplateType(
    val dbName: String,
) {
    TRANSACTIONAL("TRANSACTIONAL"),
    MARKETING("MARKETING"),
    HEADLESS("HEADLESS"),
}
