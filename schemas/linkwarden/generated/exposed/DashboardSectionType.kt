package linkwarden.models

enum class DashboardSectionType(
    val dbName: String,
) {
    STATS("STATS"),
    RECENT_LINKS("RECENT_LINKS"),
    PINNED_LINKS("PINNED_LINKS"),
    COLLECTION("COLLECTION"),
}
