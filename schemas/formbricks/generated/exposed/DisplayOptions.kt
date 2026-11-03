package formbricks.models

enum class DisplayOptions(
    val dbName: String,
) {
    DISPLAY_ONCE("displayOnce"),
    DISPLAY_MULTIPLE("displayMultiple"),
    DISPLAY_SOME("displaySome"),
    RESPOND_MULTIPLE("respondMultiple"),
}
