package teable.models

enum class SpaceDataDbBindingState(
    val dbName: String,
) {
    READY("ready"),
    VALIDATING("validating"),
    INITIALIZING("initializing"),
    MIGRATING("migrating"),
    ERROR("error"),
    DISABLED("disabled"),
}
