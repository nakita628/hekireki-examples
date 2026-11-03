package rallly.models

enum class ParticipantVisibility(
    val dbName: String,
) {
    FULL("full"),
    SCORES_ONLY("scoresOnly"),
    LIMITED("limited"),
}
