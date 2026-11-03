package teable.models

enum class SpaceDataDbMigrationJobState(
    val dbName: String,
) {
    PENDING("pending"),
    WAITING_WORKER("waiting_worker"),
    PREFLIGHT("preflight"),
    FREEZING_WRITES("freezing_writes"),
    COPYING("copying"),
    VALIDATING("validating"),
    SWITCHING("switching"),
    SUCCEEDED("succeeded"),
    FAILED("failed"),
    CANCELED("canceled"),
    ROLLED_BACK("rolled_back"),
}
