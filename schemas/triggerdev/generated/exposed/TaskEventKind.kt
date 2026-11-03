package triggerdev.models

enum class TaskEventKind(
    val dbName: String,
) {
    UNSPECIFIED("UNSPECIFIED"),
    INTERNAL("INTERNAL"),
    SERVER("SERVER"),
    CLIENT("CLIENT"),
    PRODUCER("PRODUCER"),
    CONSUMER("CONSUMER"),
    UNRECOGNIZED("UNRECOGNIZED"),
    LOG("LOG"),
}
