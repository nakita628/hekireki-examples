package formbricks.models

enum class WidgetPlacement(
    val dbName: String,
) {
    BOTTOM_LEFT("bottomLeft"),
    BOTTOM_RIGHT("bottomRight"),
    TOP_LEFT("topLeft"),
    TOP_RIGHT("topRight"),
    CENTER("center"),
}
