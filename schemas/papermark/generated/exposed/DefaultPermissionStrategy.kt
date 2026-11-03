package papermark.models

enum class DefaultPermissionStrategy(
    val dbName: String,
) {
    INHERIT_FROM_PARENT("INHERIT_FROM_PARENT"),
    ASK_EVERY_TIME("ASK_EVERY_TIME"),
    HIDDEN_BY_DEFAULT("HIDDEN_BY_DEFAULT"),
}
