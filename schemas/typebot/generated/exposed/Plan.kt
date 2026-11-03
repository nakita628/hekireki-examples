package typebot.models

enum class Plan(
    val dbName: String,
) {
    FREE("FREE"),
    STARTER("STARTER"),
    PRO("PRO"),
    LIFETIME("LIFETIME"),
    OFFERED("OFFERED"),
    CUSTOM("CUSTOM"),
    UNLIMITED("UNLIMITED"),
    ENTERPRISE("ENTERPRISE"),
}
