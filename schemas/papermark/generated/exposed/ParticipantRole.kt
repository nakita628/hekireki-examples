package papermark.models

enum class ParticipantRole(
    val dbName: String,
) {
    OWNER("OWNER"),
    PARTICIPANT("PARTICIPANT"),
}
