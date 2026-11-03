package ghostfolio.models

enum class AssetSubClass(
    val dbName: String,
) {
    BOND("BOND"),
    CASH("CASH"),
    COLLECTIBLE("COLLECTIBLE"),
    COMMODITY("COMMODITY"),
    CRYPTOCURRENCY("CRYPTOCURRENCY"),
    ETF("ETF"),
    LOAN("LOAN"),
    MUTUALFUND("MUTUALFUND"),
    PRECIOUS_METAL("PRECIOUS_METAL"),
    PRIVATE_EQUITY("PRIVATE_EQUITY"),
    STOCK("STOCK"),
}
