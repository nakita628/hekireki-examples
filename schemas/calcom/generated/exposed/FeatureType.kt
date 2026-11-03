package calcom.models

enum class FeatureType(
    val dbName: String,
) {
    RELEASE("RELEASE"),
    EXPERIMENT("EXPERIMENT"),
    OPERATIONAL("OPERATIONAL"),
    KILL_SWITCH("KILL_SWITCH"),
    PERMISSION("PERMISSION"),
}
