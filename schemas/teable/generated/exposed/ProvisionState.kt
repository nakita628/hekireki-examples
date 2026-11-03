package teable.models

enum class ProvisionState(
    val dbName: String,
) {
    PENDING("pending"),
    READY("ready"),
    ERROR("error"),
    DELETING("deleting"),
}
