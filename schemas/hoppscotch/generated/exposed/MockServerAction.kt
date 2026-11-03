package hoppscotch.models

enum class MockServerAction(
    val dbName: String,
) {
    CREATED("CREATED"),
    DELETED("DELETED"),
    ACTIVATED("ACTIVATED"),
    DEACTIVATED("DEACTIVATED"),
}
