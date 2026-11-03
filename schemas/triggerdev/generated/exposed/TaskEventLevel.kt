package triggerdev.models

enum class TaskEventLevel(
    val dbName: String,
) {
    TRACE("TRACE"),
    DEBUG("DEBUG"),
    LOG("LOG"),
    INFO("INFO"),
    WARN("WARN"),
    ERROR("ERROR"),
}
