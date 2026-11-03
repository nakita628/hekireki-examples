package teable.models

enum class BaseDataDbMoveJobState(
    val dbName: String,
) {
    PENDING("pending"),
    WAITING_WORKER("waiting_worker"),
    COPYING_BASE_SCHEMA("copying_base_schema"),
    COPYING_SHARED_ROWS("copying_shared_rows"),
    VALIDATING("validating"),
    SWITCHING("switching"),
    SUCCEEDED("succeeded"),
    FAILED("failed"),
    CANCELLED("cancelled"),
}
