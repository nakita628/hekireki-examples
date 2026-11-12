package triggerdev.models

enum class EnvironmentKeyWrappingProvider(
    val dbName: String,
) {
    PLATFORM_KEYRING("PLATFORM_KEYRING"),
    AWS_KMS("AWS_KMS"),
}
