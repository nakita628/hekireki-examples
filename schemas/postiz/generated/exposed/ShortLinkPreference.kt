package postiz.models

enum class ShortLinkPreference(
    val dbName: String,
) {
    ASK("ASK"),
    YES("YES"),
    NO("NO"),
}
