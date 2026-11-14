package langwatch.models

enum class GrantPrincipalType(
    val dbName: String,
) {
    USER("USER"),
    API_KEY("API_KEY"),
    GROUP("GROUP"),
    TEAM("TEAM"),
    ORGANIZATION("ORGANIZATION"),
    PROJECT("PROJECT"),
    ANYONE("ANYONE"),
}
