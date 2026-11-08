package plunk.models

enum class AuthMethod(
    val dbName: String,
) {
    PASSWORD("PASSWORD"),
    GOOGLE_OAUTH("GOOGLE_OAUTH"),
    GITHUB_OAUTH("GITHUB_OAUTH"),
}
