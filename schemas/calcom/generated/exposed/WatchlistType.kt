package calcom.models

enum class WatchlistType(
    val dbName: String,
) {
    EMAIL("EMAIL"),
    DOMAIN("DOMAIN"),
    USERNAME("USERNAME"),
}
