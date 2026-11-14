package langwatch.models

enum class ConnectedCreditGrantKind(
    val dbName: String,
) {
    COMMIT("commit"),
    ADDED("added"),
    RENEWAL("renewal"),
}
