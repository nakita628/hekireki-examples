package linkwarden.models

enum class LinksRouteTo(
    val dbName: String,
) {
    ORIGINAL("ORIGINAL"),
    PDF("PDF"),
    READABLE("READABLE"),
    MONOLITH("MONOLITH"),
    SCREENSHOT("SCREENSHOT"),
    DETAILS("DETAILS"),
}
