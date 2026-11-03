package ghostfolio.models

enum class MarketDataState(
    val dbName: String,
) {
    CLOSE("CLOSE"),
    INTRADAY("INTRADAY"),
}
