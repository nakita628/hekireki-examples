package formbricks.models

enum class ChartType(
    val dbName: String,
) {
    AREA("area"),
    BAR("bar"),
    PIE("pie"),
    BIG_NUMBER("big_number"),
    MATRIX("matrix"),
}
