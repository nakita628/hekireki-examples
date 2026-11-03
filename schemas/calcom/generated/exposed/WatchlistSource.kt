package calcom.models

enum class WatchlistSource(
    val dbName: String,
) {
    MANUAL("MANUAL"),
    FREE_DOMAIN_POLICY("FREE_DOMAIN_POLICY"),
    SIGNUP("SIGNUP"),
}
