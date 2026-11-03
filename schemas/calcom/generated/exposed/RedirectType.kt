package calcom.models

enum class RedirectType(
    val dbName: String,
) {
    UserEventType("user-event-type"),
    TeamEventType("team-event-type"),
    User("user"),
    Team("team"),
}
