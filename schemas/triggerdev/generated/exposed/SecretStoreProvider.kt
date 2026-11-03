package triggerdev.models

enum class SecretStoreProvider(
    val dbName: String,
) {
    DATABASE("DATABASE"),
    AWS_PARAM_STORE("AWS_PARAM_STORE"),
}
