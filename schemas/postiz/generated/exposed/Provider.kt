package postiz.models

enum class Provider(
    val dbName: String,
) {
    LOCAL("LOCAL"),
    GITHUB("GITHUB"),
    GOOGLE("GOOGLE"),
    APPLE("APPLE"),
    FARCASTER("FARCASTER"),
    WALLET("WALLET"),
    GENERIC("GENERIC"),
}
