package jitsu.models

enum class ReverseSyncExtraction(
    val dbName: String,
) {
    CURSOR("cursor"),
    FULL("full"),
}
