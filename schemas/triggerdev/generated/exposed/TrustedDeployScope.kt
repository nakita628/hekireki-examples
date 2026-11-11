package triggerdev.models

enum class TrustedDeployScope(
    val dbName: String,
) {
    ANY("ANY"),
    BRANCH("BRANCH"),
    TAG("TAG"),
    ENVIRONMENT("ENVIRONMENT"),
}
