package jitsu.models

enum class ReverseSyncMode(
    val dbName: String,
) {
    UPSERT("upsert"),
    MIRROR("mirror"),
}
