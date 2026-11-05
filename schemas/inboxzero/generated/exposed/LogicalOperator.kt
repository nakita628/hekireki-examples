package inboxzero.models

enum class LogicalOperator(
    val dbName: String,
) {
    AND("AND"),
    OR("OR"),
}
