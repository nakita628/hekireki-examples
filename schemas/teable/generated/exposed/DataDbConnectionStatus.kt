package teable.models

enum class DataDbConnectionStatus(
    val dbName: String,
) {
    PENDING("pending"),
    VALIDATING("validating"),
    READY("ready"),
    ERROR("error"),
    MIGRATING("migrating"),
    DISABLED("disabled"),
}
