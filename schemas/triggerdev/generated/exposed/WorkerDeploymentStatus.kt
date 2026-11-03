package triggerdev.models

enum class WorkerDeploymentStatus(
    val dbName: String,
) {
    PENDING("PENDING"),
    INSTALLING("INSTALLING"),
    BUILDING("BUILDING"),
    DEPLOYING("DEPLOYING"),
    DEPLOYED("DEPLOYED"),
    FAILED("FAILED"),
    CANCELED("CANCELED"),
    TIMED_OUT("TIMED_OUT"),
}
