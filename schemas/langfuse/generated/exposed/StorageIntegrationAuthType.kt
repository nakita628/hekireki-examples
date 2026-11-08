package langfuse.models

enum class StorageIntegrationAuthType(
    val dbName: String,
) {
    ACCESS_KEYS("ACCESS_KEYS"),
    ROLE_ARN("ROLE_ARN"),
}
