package langwatch.models

enum class SsoConnectionMigrationPhase(
    val dbName: String,
) {
    SETUP("SETUP"),
    GRACE_LEGACY("GRACE_LEGACY"),
    GRACE_DIRECT("GRACE_DIRECT"),
    FINALIZING("FINALIZING"),
    FINALIZED("FINALIZED"),
}
