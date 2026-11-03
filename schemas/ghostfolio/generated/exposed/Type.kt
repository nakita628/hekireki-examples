package ghostfolio.models

enum class Type(
    val dbName: String,
) {
    BUY("BUY"),
    DIVIDEND("DIVIDEND"),
    FEE("FEE"),
    INTEREST("INTEREST"),
    LIABILITY("LIABILITY"),
    SELL("SELL"),
}
