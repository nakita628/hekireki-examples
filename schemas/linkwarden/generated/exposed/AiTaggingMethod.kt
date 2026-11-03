package linkwarden.models

enum class AiTaggingMethod(
    val dbName: String,
) {
    DISABLED("DISABLED"),
    GENERATE("GENERATE"),
    EXISTING("EXISTING"),
    PREDEFINED("PREDEFINED"),
}
