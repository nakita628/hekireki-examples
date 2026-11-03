package ghostfolio.models

enum class AssetClass(
    val dbName: String,
) {
    ALTERNATIVE_INVESTMENT("ALTERNATIVE_INVESTMENT"),
    COMMODITY("COMMODITY"),
    EQUITY("EQUITY"),
    FIXED_INCOME("FIXED_INCOME"),
    LIQUIDITY("LIQUIDITY"),
    REAL_ESTATE("REAL_ESTATE"),
}
